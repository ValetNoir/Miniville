using Miniville.Enums;
using Miniville.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Miniville.BuildingStuff
{
    internal static class EffectsGenerator
    {

        #region blue/red/green cards effects 
        public static Action<Player[], int, int> GenerateMoney(int amount)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];
                owner.Money += amount;

                Console.WriteLine($"Player {ownerIndex} receives {amount} coins from the bank.");
            };
        }

        public static Action<Player[], int, int> GenerateMoneyPerType(int amount, CardType type)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];
                int count = owner.Cards.Where(card => card.Type == type).Count();
                owner.Money += amount * count;

                Console.WriteLine($"Player {ownerIndex} receives {amount * count} coins from the bank for owning {count} {type} buildings.");
            };
        }

        public static Action<Player[], int, int> StealMoneyFromCurrentPlayer(int amount)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];
                var currentPlayer = players[currentPlayerIndex];
                if (currentPlayer.Money < amount)
                {
                    amount = currentPlayer.Money;
                }
                currentPlayer.Money -= amount;
                owner.Money += amount;

                Console.WriteLine($"Player {ownerIndex} steals {amount} coins from Player {currentPlayerIndex}.");
            };
        }
        #endregion

        #region purple cards effects
        public static Action<Player[], int, int> StealMoneyFromAll(int amount)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];

                var total = 0;
                foreach (var player in players)
                {
                    if (player != owner)
                    {
                        int stealAmount = amount;
                        if (player.Money < amount)
                        {
                            stealAmount = player.Money;
                        }
                        player.Money -= stealAmount;
                        total += stealAmount;
                    }
                }
                owner.Money += total;

                Console.WriteLine($"Player {ownerIndex} steals a total of {total} coins from all other players.");
            };
        }

        public static Action<Player[], int, int> StealMoneyFromChosen(int amount)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];

                int chosenPlayerIndex = -1;

                if (owner.Type == PlayerType.HUMAN)
                {
                    string playerPrompt = $"Choose a player to steal {amount} coins from:\n";
                    for (int i = 0; i < players.Length; i++)
                    {
                        var player = players[i];
                        if (player != owner)
                            playerPrompt += $"Player {i}: (Money: {player.Money})\n";
                    }

                    chosenPlayerIndex = HumanInterface.AskIndex(playerPrompt, players.Length, ownerIndex);
                    Console.WriteLine($"You have chosen Player {chosenPlayerIndex}.");

                }
                else if (owner.Type == PlayerType.BOT)
                {
                    chosenPlayerIndex = BotInterface.AskIndex(players.Length, ownerIndex);
                    Console.WriteLine($"Bot Player {ownerIndex} has chosen Player {chosenPlayerIndex} to steal from.");
                }

                Player chosenPlayer = players[chosenPlayerIndex];
                int stealAmount = amount;
                if (chosenPlayer.Money < amount)
                    stealAmount = chosenPlayer.Money;

                chosenPlayer.Money -= stealAmount;
                owner.Money += stealAmount;

            };
        }

        public static Action<Player[], int, int> TradeBuilding()
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];

                bool canTrade = owner.Cards.Count > 0 && players.Any(p => p != owner && p.Cards.Count > 0);

                if (!canTrade)
                {
                    Console.WriteLine("Trade impossible. Not enough buildings to trade.");
                    return;
                }

                int targetIndex = -1;
                int targetBuildingIndex = -1;
                int ownerBuildingIndex = -1;

                if (owner.Type == PlayerType.HUMAN)
                {
                    bool wantsToTrade = HumanInterface.AskBool("Do you want to trade a building with another player ?");
                
                    if (!wantsToTrade)
                    {
                        Console.WriteLine("Trade cancelled.");
                        return;
                    }

                    Console.WriteLine("Proceeding with trade...");

                    string targetPrompt = $"Choose a player to steal to trade with:\n";
                    for (int i = 0; i < players.Length; i++)
                    {
                        var player = players[i];
                        if (player != owner)
                            targetPrompt += $"Player {i}\n";
                    }
                    targetPrompt += "Enter player number: ";

                    targetIndex = HumanInterface.AskIndex(targetPrompt, players.Length, ownerIndex);

                    Player target = players[targetIndex];
                    Console.WriteLine($"You have chosen Player {targetIndex}.");

                    string targetBuildingPrompt = $"Choose one of their building:\n";
                    for (int i = 0; i < target.Cards.Count; i++)
                    {
                        var card = target.Cards[i];
                        targetBuildingPrompt += $"{i} : {card.Name}\n";
                    }
                    targetBuildingPrompt += "Enter building number: ";

                    targetBuildingIndex = HumanInterface.AskIndex(targetBuildingPrompt, target.Cards.Count);

                    Card targetBuilding = target.Cards[targetBuildingIndex];
                    Console.WriteLine($"You have chosen to receive their {targetBuilding.Name}.");

                    string ownerBuildingPrompt = $"Choose one of your buildings:\n";
                    for (int i = 0; i < owner.Cards.Count; i++)
                    {
                        var card = owner.Cards[i];
                        ownerBuildingPrompt += $"{i} : {card.Name}\n";
                    }
                    ownerBuildingPrompt += "Enter building number: ";

                    ownerBuildingIndex = HumanInterface.AskIndex(ownerBuildingPrompt, owner.Cards.Count);

                    Card ownerBuilding = owner.Cards[ownerBuildingIndex];
                    Console.WriteLine($"You have chosen to give your {ownerBuilding.Name}.");

                    owner.Cards.Remove(ownerBuilding);
                    target.Cards.Add(ownerBuilding);

                    target.Cards.Remove(targetBuilding);
                    owner.Cards.Add(targetBuilding);

                    Console.WriteLine($"Trade completed: Player {ownerIndex} traded their {ownerBuilding.Name} for Player {targetIndex}'s {targetBuilding.Name}.");
                }
                else if(owner.Type == PlayerType.BOT)
                {
                    bool wantsToTrade = BotInterface.AskBool();

                    if (!wantsToTrade)
                    {
                        Console.WriteLine("Bot doesn't wan to trade.");
                        return;
                    }

                    targetIndex = BotInterface.AskIndex(players.Length, ownerIndex);
                    Player target = players[targetIndex];
                    ownerBuildingIndex = BotInterface.AskIndex(owner.Cards.Count);
                    targetBuildingIndex = BotInterface.AskIndex(target.Cards.Count);

                    Card ownerBuilding = owner.Cards[ownerBuildingIndex];
                    Card targetBuilding = target.Cards[targetBuildingIndex];

                    owner.Cards.Remove(ownerBuilding);
                    target.Cards.Add(ownerBuilding);

                    target.Cards.Remove(targetBuilding);
                    owner.Cards.Add(targetBuilding);

                    Console.WriteLine($"Trade completed: Player {ownerIndex} traded their {ownerBuilding.Name} for Player {targetIndex}'s {targetBuilding.Name}.");
                }

            };
        }
        #endregion
    }
}

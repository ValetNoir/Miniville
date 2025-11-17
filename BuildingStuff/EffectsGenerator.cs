using Miniville.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville.BuildingStuff
{
    internal class EffectsGenerator
    {
        public static readonly EffectsGenerator Instance = new EffectsGenerator();
        public EffectsGenerator() { }

        public Action<Player[], int, int> GenerateMoney(int amount)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];
                owner.Money += amount;

                Console.WriteLine($"Player {ownerIndex} receives {amount} coins from the bank.");
            };
        }

        public Action<Player[], int, int> GenerateMoneyPerType(int amount, CardType type)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];
                int count = owner.Cards.Where(card => card.Type == type).Count();
                owner.Money += amount * count;

                Console.WriteLine($"Player {ownerIndex} receives {amount * count} coins from the bank for owning {count} {type} buildings.");
            };
        }

        public Action<Player[], int, int> StealMoneyFromCurrentPlayer(int amount)
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

        public Action<Player[], int, int> StealMoneyFromAll(int amount)
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

        public Action<Player[], int, int> StealMoneyFromChosen(int amount)
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];

                Player chosenPlayer = null;
                bool isChoosing = true;
                while (isChoosing)
                {
                    Console.WriteLine("Choose a player to steal from:");
                    for (int i = 0; i < players.Length; i++)
                    {
                        var player = players[i];
                        if (player != owner)
                            Console.WriteLine($"Player {i}: (Money: {player.Money})");
                    }

                    Console.Write("Enter player number: ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int chosenIndex) && chosenIndex >= 0 && chosenIndex < players.Length)
                    {
                        if (chosenIndex != ownerIndex)
                        {
                            chosenPlayer = players[chosenIndex];
                            Console.WriteLine($"You have chosen Player {chosenIndex}.");
                            isChoosing = false;
                        }
                        else
                            Console.WriteLine("Invalid player index. Please choose a player that is not yourself.");
                    }
                    else
                        Console.WriteLine("Invalid input. Please enter a valid player number.");
                }

                int stealAmount = amount;
                if (chosenPlayer.Money < amount)
                    stealAmount = chosenPlayer.Money;

                chosenPlayer.Money -= stealAmount;
                owner.Money += stealAmount;
            };
        }
        

        public Action<Player[], int, int> TradeBuilding()
        {
            return (players, ownerIndex, currentPlayerIndex) =>
            {
                var owner = players[ownerIndex];

                bool canTrade = owner.Cards.Count > 0 && players.Any(p => p != owner && p.Cards.Count > 0);

                if(!canTrade)
                {
                    Console.WriteLine("Trade not possible. Either you or all other players have no buildings to trade.");
                    return;
                }

                bool wantsToTrade = false;
                while (true)
                {
                    Console.Write("Do you want to trade a building with another player? (y/n): ");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "y")
                    {
                        wantsToTrade = true;
                        break;
                    }
                    else if (input.ToLower() == "n")
                    {
                        wantsToTrade = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                    }
                }
                
                if (!wantsToTrade)
                {
                    Console.WriteLine("Trade cancelled.");
                    return;
                }

                Console.WriteLine("Proceeding with trade...");

                Player chosenPlayer = null;
                bool isChoosing = true;
                while (isChoosing)
                {
                    Console.WriteLine("Choose a player to trade with:");
                    for (int i = 0; i < players.Length; i++)
                    {
                        var player = players[i];
                        if (player != owner)
                            Console.WriteLine($"Player {i}");
                    }

                    Console.Write("Enter player number: ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int chosenIndex) && chosenIndex >= 0 && chosenIndex < players.Length)
                    {
                        if (chosenIndex != ownerIndex)
                        {
                            chosenPlayer = players[chosenIndex];
                            Console.WriteLine($"You have chosen Player {chosenIndex}.");
                            isChoosing = false;
                        }
                        else
                            Console.WriteLine("Invalid player index. Please choose a player that is not yourself.");
                    }
                    else
                        Console.WriteLine("Invalid input. Please enter a valid player number.");
                }

                Card chosenPlayerBuildingToTrade = null;
                bool isChoosingChosenPlayerBuilding = true;
                while (isChoosingChosenPlayerBuilding)
                {
                    Console.WriteLine("Choose one of THEIR buildings to trade:");
                    Console.WriteLine("(You will receive this)");
                    for (int i = 0; i < chosenPlayer.Cards.Count; i++)
                    {
                        var card = chosenPlayer.Cards[i];
                        Console.WriteLine($"{i} : {card.Name}");
                    }
                    Console.Write("Enter building number: ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int buildingIndex) && buildingIndex >= 0 && buildingIndex < chosenPlayer.Cards.Count)
                    {
                        chosenPlayerBuildingToTrade = chosenPlayer.Cards[buildingIndex];
                        Console.WriteLine($"You have chosen to receive their {chosenPlayerBuildingToTrade.Name}.");
                        isChoosingChosenPlayerBuilding = false;
                    }
                    else
                        Console.WriteLine("Invalid input. Please enter a valid building number.");
                }

                Card ownerBuildingToTrade = null;
                bool isChoosingOwnerBuilding = true;
                while (isChoosingOwnerBuilding)
                {
                    Console.WriteLine("Choose one of YOUR buildings to trade:");
                    Console.WriteLine("(You will give this)");
                    for (int i = 0; i < owner.Cards.Count; i++)
                    {
                        var card = owner.Cards[i];
                        Console.WriteLine($"{i} : {card.Name}");
                    }
                    Console.Write("Enter building number: ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int buildingIndex) && buildingIndex >= 0 && buildingIndex < owner.Cards.Count)
                    {
                        ownerBuildingToTrade = owner.Cards[buildingIndex];
                        Console.WriteLine($"You have chosen to trade your {ownerBuildingToTrade.Name}.");
                        isChoosingOwnerBuilding = false;
                    }
                    else
                        Console.WriteLine("Invalid input. Please enter a valid building number.");
                }

                owner.Cards.Remove(ownerBuildingToTrade);
                chosenPlayer.Cards.Add(ownerBuildingToTrade);

                chosenPlayer.Cards.Remove(chosenPlayerBuildingToTrade);
                owner.Cards.Add(chosenPlayerBuildingToTrade);
            };
        }

    }
}

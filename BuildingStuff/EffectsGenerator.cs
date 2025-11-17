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

        public Func<Player[], int, int, bool> GenerateMoney(int amount)
        {
            return (gameState, ownerIndex, currentPlayerIndex) =>
            {
                var owner = gameState.Players[ownerIndex];
                owner.Money += amount;

                return true;
            };
        }

        public Func<Player[], int, int, bool> StealMoneyFromCurrentPlayer(int amount)
        {
            return (gameState, ownerIndex, currentPlayerIndex) =>
            {
                var owner = gameState.Players[ownerIndex];
                var currentPlayer = gameState.Players[currentPlayerIndex];
                if (currentPlayer.Money < amount)
                {
                    amount = currentPlayer.Money;
                }
                currentPlayer.Money -= amount;
                owner.Money += amount;

                return true;
            };
        }

        public Func<Player[], int, int, bool> GenerateMoneyPerType(int amount, CardType type)
        {
            return (gameState, ownerIndex, currentPlayerIndex) =>
            {
                var owner = gameState.Players[ownerIndex];
                int count = owner.Cards.Where(card => card.Type == type).Count();
                owner.Money += amount * count;

                return true;
            };
        }

        public Func<Player[], int, int, bool> StealMoneyFromAll(int amount)
        {
            return (gameState, ownerIndex, currentPlayerIndex) =>
            {
                var owner = gameState.Players[ownerIndex];
                foreach (var player in gameState.Players)
                {
                    if (player != owner)
                    {
                        int stealAmount = amount;
                        if (player.Money < amount)
                        {
                            stealAmount = player.Money;
                        }
                        player.Money -= stealAmount;
                        owner.Money += stealAmount;
                    }
                }
                return true;
            };
        }

        public Func<Player[], int, int, bool> StealMoneyFromChosen(int amount)
        {
            return (gameState, ownerIndex, currentPlayerIndex) =>
            {
                var owner = gameState.Players[ownerIndex];
                Player chosenPlayer = null;

                bool isChoosing = true;
                while (isChoosing)
                {
                    Console.WriteLine("Choose a player to steal from:");
                    for (int i = 0; i < gameState.Players.Length; i++)
                    {
                        var player = gameState.Players[i];
                        if (player != owner)
                            Console.WriteLine($"Player {i}: (Money: {player.Money})");
                    }

                    Console.Write("Enter player number: ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int chosenIndex) && chosenIndex >= 0 && chosenIndex < gameState.Players.Length)
                    {
                        if (chosenIndex != ownerIndex)
                        {
                            chosenPlayer = gameState.Players[chosenIndex];
                            isChoosing = false;
                        }
                        else
                            Console.WriteLine("Invalid player index. Please choose a player that is not yourself.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid player number.");
                    }
                }

                int stealAmount = amount;
                if (chosenPlayer.Money < amount)
                {
                    stealAmount = chosenPlayer.Money;
                }
                chosenPlayer.Money -= stealAmount;
                owner.Money += stealAmount;

                return true;
            };
        }

    }
}

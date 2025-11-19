using Miniville.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville.BuildingStuff
{
    internal class Card
    {
        public readonly int[] ActiveNumbers;
        public readonly CardColor Color;
        public readonly CardType Type;
        public readonly string Name;
        public readonly string Desc;
        public readonly int Price;
        public readonly Action<Player[], int, int> Effect; // Player receiver, Player giver = null

        public Card(int[] activeNumbers, CardColor color, CardType type, string name, string desc, int price, Action<Player[], int, int> effect)
        {
            ActiveNumbers = activeNumbers;
            Color = color;
            Type = type;
            Name = name;
            Desc = desc;
            Price = price;
            Effect = (players, ownerIndex, currentPlayerIndex) =>
            {
                Console.WriteLine($"Activating {Name} for Player {ownerIndex} ({players[ownerIndex].Name})");
                effect(players, ownerIndex, currentPlayerIndex);
                Console.WriteLine("");
            };
        }
    }
}

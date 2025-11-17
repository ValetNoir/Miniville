using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class Card
    {
        public readonly int[] ActiveNumbers;
        public readonly CardColor Color;
        public readonly CardType Type;
        public readonly string Name;
        public readonly string Desc;
        public readonly int Price;
        public readonly Func<Player, Player, bool> Effect; // Player receiver, Player giver = null

        Card(int[] activeNumbers, CardColor color, CardType type, string name, string desc, int price, Func<Player, Player, bool> effect)
        {
            ActiveNumbers = activeNumbers;
            Color = color;
            Type = type;
            Name = name;
            Desc = desc;
            Price = price;
            Effect = effect;
        }
    }
}

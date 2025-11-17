using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    class Card
    {
        public readonly int[] ActiveNumbers;
        public readonly CardColor Color;
        public readonly CardType Type;
        public readonly string Name;
        public readonly string Desc;
        public readonly int Price;

        Card(int[] activeNumbers, CardColor color, CardType type, string name, string desc, int price, int gain, Building gainBuildingType=null)
        {
            ActiveNumbers = activeNumbers;
            Color = color;
            Type = type;
            Name = name;
            Desc = desc;
            Price = price;
         }

        public void Effect(Player source=null, Player target)
        {
            //magie noire gaspard (ça rime)
        }
    }
}

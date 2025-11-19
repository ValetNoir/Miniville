using Miniville.BuildingStuff;
using Miniville.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class Player
    {
        public List<Card> Cards = new List<Card>();
        public int Money;
        public readonly string Name;
        public readonly PlayerType Type;

        public Player(PlayerType playerType = PlayerType.HUMAN, string name = "machin")
        {
            Name = name;
            Type = playerType;
            Money = 3;
            Cards.Add(Buildings.WHEAT_FIELD);
            Cards.Add(Buildings.BAKERY);
        }

        public void BuyCard(Card building) //Check if the player have enough money and add the card to the list if yes
        {
            if (this.Money >= building.Price)
            {
                this.Money -= building.Price;
                this.Cards.Add(building);
                Console.WriteLine($"{this.Name} bought a {building.Name}");
            }
            else
            {
                Console.WriteLine($"Not enough money. Bank issue on {this.Name} side loooser");
            }
        }
        public bool HasBuilding(Card building) //Return if the player have the building or not
        {
            return Cards.Contains(building) ? true : false;
        }
        public bool HasWon() //Return if the player has 20 coins to win or not
        {
            return this.Money >= 20 ? true : false;
        }

		public override string ToString()
		{
			return Name;
		}

        public void DisplayCards()
        {
            Console.WriteLine($"{this.Name}'s cards are:");
            foreach (Card card in Cards)
            {
                Console.Write("[ ");
                foreach(var number in card.ActiveNumbers)
                {
                    Console.Write($"{number} ");
                }
                Console.WriteLine($"] {card.Color} - {card.Name} : {card.Desc}");
            }
        }
    }
}

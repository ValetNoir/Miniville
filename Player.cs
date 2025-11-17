using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class Player
    {
        private List<Card> cards = new List<Card>();
        int money = 0;

        public Player() 
        {
            cards.Add(new Card([1],cards.Blue, Buildings.Wheat_fields));
            cards.Add(new Card([1,2] card.Green, Buildings.Bakery))
        }

        public void BuyCard(Card building) //Check if the player have enough money and add the card to the list if yes
        {
            if (this.money >= building.Price)
            {
                this.money -= building.Price;
                this.cards.Add(building);
            }
        }
        public bool HasBuilding(Card building) //Return if the player have the building or not
        {
            return cards.Contains(building) ? true : false;
        }
        public bool HasWon() //Return if the player has 20 coins to win or not
        {
            return this.money >= 20 ? true : false;
        }
    }
}

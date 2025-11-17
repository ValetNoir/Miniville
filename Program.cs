using System;

namespace Miniville
{
    static class CardsCollection
	{
		static readonly Card FERME = new();
	}
    class Card {}
    
    class Pile
    {
        private List<Card> Cards = [];
        public Pile() {}

        public void Stack(Card newCardReference)
        {
            Cards.Add(newCardReference);
        }
        
        public Card Top()
        {
            return Cards[Cards.Count];
        }
        
        public Card Pickup()
        {
            Card pickedUpCardReference = Cards[Cards.Count];
            Cards.RemoveAt(Cards.Count);
            return pickedUpCardReference;
        }
        
        public List<Card> Loop()
        {
            return Cards;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Pile cardPile = new();
            foreach (Card loopCard in cardPile.Loop())
			{
				
			}
        }
    }
}
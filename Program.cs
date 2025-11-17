using System;

namespace Miniville
{
    
    class Card {}
    
    class Pile
    {
        private Card[] Cards = [];
        public Pile() {}

        public void Stack(Card newCardReference)
        {
            Cards.Append(newCardReference);
        }
        
        public Card Top()
        {
            return Cards[Cards.Length];
        }
        
        public Card[] Loop()
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
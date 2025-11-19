using Miniville.BuildingStuff;

namespace Miniville
{
	class Pile
	{
		public List<Card> Cards = [];
		public Pile(Card newCardReference)
		{
            for (int i = 0; i < 6; i++)
            {
                Cards.Add(newCardReference);
            }
        }

		public void Stack(Card newCardReference)
		{
			Cards.Add(newCardReference);
		}

        public bool IsEmpty()
		{
			return Cards.Count == 0;
		}

		public Card Top()
		{
			return Cards[Cards.Count - 1];
		}

		public Card Pickup()
		{
			Card pickedUpCardReference = Cards[Cards.Count-1];
			Cards.RemoveAt(Cards.Count-1);
			return pickedUpCardReference;
		}

		public List<Card> Loop()
		{
			return Cards;
		}
	}
}
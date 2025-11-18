using Miniville.BuildingStuff;

namespace Miniville
{
	class Pile
	{
		public List<Card> Cards = [];
		public Pile() {}

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
}
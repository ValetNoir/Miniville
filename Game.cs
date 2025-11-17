using Miniville.BuildingStuff;

namespace Miniville
{
	class Game
	{
		private Random rand = new Random();
		public Player[] Players = new Player[2];
		public readonly int CurrentTurnPlayerIndex;
		public Dictionary<Card, int> BuildingsAmountLeft = new Dictionary<Card, int>();

		public void PlayTurn()
		{
			while (!Players[0].HasWon() || !Players[1].HasWon())
			{
				int diceResult = RollDices(7);
				//Check if Player B have Blue or Red Card
				//Check if Player A have Blue or Green Card
				//Player A can buy a card
			}
		}

		public int RollDices(int dicesNumber)
		{
			return rand.Next(dicesNumber);
		}
	}
}
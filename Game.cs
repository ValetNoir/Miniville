using Miniville.BuildingStuff;
using Miniville.Enums;

namespace Miniville
{
	class Game
	{
		public List<Player> Players = [];
		public readonly int CurrentTurnPlayerIndex;
		public Dictionary<Card, int> BuildingsAmountLeft = new Dictionary<Card, int>();
		private Dice dice = new();
		private int turns = 0;

		public Game(int humanAmount = 1, int botAmount = 1)
		{
			// setup
			for (int i = 0; i < humanAmount; i++)
			{
				string actualName = (humanAmount > 1) ? $"humain {i+1}" : "humain";
				Players.Add(new Player(PlayerType.HUMAN, actualName));
			}
			for (int i = 0; i < botAmount; i++)
			{
				string actualName = (humanAmount > 1) ? $"bot {i+1}" : "bot";
				Players.Add(new Player(PlayerType.BOT, actualName));
			}

			// play
			Loop();
		}

		public void Loop()
		{
			while (!Ended())
			{
				// get the playing player and the others
				turns++;
				int playingIndex = turns % Players.Count;
				Player playingPlayer = Players[playingIndex];
				IEnumerable<Player> otherPlayers = Players.Where((val, idx) => idx != playingIndex);

				// roooool the dice yea
				int diceResult = dice.Roll();
				Console.WriteLine($"{playingPlayer} lance le dé et fait {diceResult} !");

				
				//Check if PlayingPlayer have Blue or Red Card
				
				Console.WriteLine($"{playingPlayer} gagne {diceResult}$ ! (test)");
				playingPlayer.Money += diceResult;
				
				//Check if OthersPlayers have Blue or Green Card
				
				//PlayingPlayer can buy a card
			}
		}

		public int? GetWinerIndex()
		{
			// get winer's index
			for (int i = 0; i < Players.Count; i++)
			{
				if (Players[i].HasWon())
				{
					return i;
				}
			}
			return null;
		}

		public bool Ended()
		{
			// if winer
			int? winerIndex = GetWinerIndex();
			if (winerIndex == null) return false;

			// win
			Console.WriteLine($"{Players[(int)winerIndex]} a gagné ! GG");
			return true;
		}
	}
}
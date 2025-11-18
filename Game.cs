using Miniville.BuildingStuff;
using Miniville.Enums;

namespace Miniville
{
	class Game
	{
		public Player[] Players = [];
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
				Players.Append(new(PlayerType.HUMAN, actualName));
			}
			for (int i = 0; i < botAmount; i++)
			{
				string actualName = (humanAmount > 1) ? $"bot {i+1}" : "bot";
				Players.Append(new(PlayerType.BOT, actualName));
			}

			// play and won
			int winerIndex = Loop();
			End(winerIndex);
		}

		public int Loop()
		{
			while (!Players[0].HasWon() || !Players[1].HasWon())
			{
				// get the playing player and the others
				turns++;
				int playingIndex = turns % Players.Length;
				Player playingPlayer = Players[playingIndex];
				IEnumerable<Player> otherPlayers = Players.Where((val, idx) => idx != playingIndex);

				// roooool the dice yea
				int diceResult = dice.Roll();
				Console.WriteLine($"{playingPlayer} lance le dé et fait {diceResult} !");

				
				//Check if PlayingPlayer have Blue or Red Card
				
				Console.WriteLine($"{playingPlayer} gagne {diceResult}$ ! (test)");
				
				//Check if OthersPlayers have Blue or Green Card
				
				//Player A can buy a card
				if (Players[0].HasWon() || Players[1].HasWon()) break;
				//Check if Player A have Blue or Red Card
				//Check if Player B have Blue or Green Card
				//Player B can buy a card
			}

			// get winer's index
			for (int i = 0; i < Players.Length; i++)
			{
				if (Players[i].HasWon())
				{
					return i;
				}
			}
			throw new Exception("out of loop without a winer");
		}

		public void End(int winerIndex)
		{
			Console.WriteLine($"{Players[winerIndex]} a gagné ! GG");
		}
	}
}
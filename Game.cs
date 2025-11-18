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
			List<Player> playersList = [];

			for (int i = 0; i < humanAmount; i++)
			{
				string actualName = (humanAmount > 1) ? $"humain {i+1}" : "humain";
				playersList.Add(new Player(PlayerType.HUMAN, actualName));
			}

			for (int i = 0; i < botAmount; i++)
			{
				string actualName = (humanAmount > 1) ? $"bot {i+1}" : "bot";
				playersList.Add(new Player(PlayerType.BOT, actualName));
			}

			Players = [.. playersList];

			// play
			Loop();
		}

		public void Loop()
		{
			while (!Ended())
			{
				// get the playing player and the others
				turns++;
				int playingIndex = turns % Players.Length;
				Player playingPlayer = Players[playingIndex];
				IEnumerable<Player> otherPlayers = Players.Where((val, idx) => idx != playingIndex);

				// roooool the dice yea
				int diceResult = dice.Roll(); // logic to choose to play with 1 or 2 dices
				Console.WriteLine($"{playingPlayer} rolled a {diceResult} !");

				foreach (Card activatedCard in playingPlayer.Cards)
				{
					activatedCard.Effect(Players, playingIndex, diceResult);
				}

				
				//Check if PlayingPlayer have Blue or Red Card
				
				Console.WriteLine($"{playingPlayer} gagne {diceResult}$ ! (test)"); // in english please 🤓☝
                playingPlayer.Money += diceResult;
				
				//Check if OthersPlayers have Blue or Green Card
				
				//PlayingPlayer can buy a card


			}
		}

		public int? GetWinerIndex()
		{
			// get winer's index
			for (int i = 0; i < Players.Length; i++)
			{
				if (Players[i].HasWon())
				{
					return i; // Equality ?
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
			Console.WriteLine($"{Players[(int)winerIndex]} won !");
			return true;
		}
	}
}
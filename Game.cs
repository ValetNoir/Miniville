using Miniville.BuildingStuff;
using Miniville.Enums;
using System.Runtime.CompilerServices;

namespace Miniville
{
	class Game
	{
		public Player[] Players = [];
		public int CurrentTurnPlayerIndex;
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
				CurrentTurnPlayerIndex = turns % Players.Length;
				Player playingPlayer = Players[CurrentTurnPlayerIndex];
				IEnumerable<Player> otherPlayers = Players.Where((val, idx) => idx != CurrentTurnPlayerIndex);

				// roooool the dice yea
				// logic to choose to play with 1 or 2 dices -> argument true for 2 dices
				bool isDoubleDice;
				if (Players[CurrentTurnPlayerIndex].Type == PlayerType.HUMAN)
					isDoubleDice = HumanInterface.AskBool("Do you want to roll 2 dices?");
				else
					isDoubleDice = BotInterface.AskBool();
                
				var diceResult = dice.Roll(isDoubleDice);
				Console.WriteLine($"{playingPlayer} rolled a {diceResult.total} !");

                //Check if PlayingPlayer have Blue or Green Card & activate card
                foreach (Card card in playingPlayer.Cards)
                {
                    if (card.Color == CardColor.BLUE || card.Color == CardColor.GREEN)
					{
                        if (card.ActiveNumbers.Length > 1) // double active number
                        {
							if (card.ActiveNumbers.Contains(diceResult.faces[0]))	// dice 1
							{
                                card.Effect(Players, CurrentTurnPlayerIndex, CurrentTurnPlayerIndex);
                            }
							if (diceResult.faces.Length > 1 && card.ActiveNumbers.Contains(diceResult.faces[1]))	// dice 2 (if it exists)
							{
                                card.Effect(Players, CurrentTurnPlayerIndex, CurrentTurnPlayerIndex);
                            }
						}
						else if (card.ActiveNumbers.Contains(diceResult.total)) // single active number
                        {
							card.Effect(Players, CurrentTurnPlayerIndex, CurrentTurnPlayerIndex);
						}
                    }
                }

                //Check if OthersPlayers have Blue or Red Card & activate card
                int ownerIndex = 0;
                foreach (Player player in Players)
				{
					if (player != playingPlayer)	// exclude playing player
					{
						foreach (Card card in player.Cards)
						{
                            if (card.Color == CardColor.BLUE || card.Color == CardColor.RED)
                            {
                                if (card.ActiveNumbers.Length > 1) // double active number
                                {
                                    if (card.ActiveNumbers.Contains(diceResult.faces[0]))   // dice 1
                                    {
                                        card.Effect(Players, ownerIndex, CurrentTurnPlayerIndex);
                                    }
                                    if (diceResult.faces.Length > 1 && card.ActiveNumbers.Contains(diceResult.faces[1]))    // dice 2 (if it exists)
                                    {
                                        card.Effect(Players, ownerIndex, CurrentTurnPlayerIndex);
                                    }
                                }
                                else if (card.ActiveNumbers.Contains(diceResult.total)) // single active number
                                {
                                    card.Effect(Players, ownerIndex, CurrentTurnPlayerIndex);
                                }
                            }
                        }
                    }
					ownerIndex++;
				}
								
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

		private void CardShop()
		{
			if (Players[CurrentTurnPlayerIndex].Type == PlayerType.HUMAN)
			{
				if (!HumanInterface.AskBool("It is shopping time! Would you like to buy a card?")) return;
			}
		}
	}
}
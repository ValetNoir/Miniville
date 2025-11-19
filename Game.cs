using Miniville.BuildingStuff;
using Miniville.Enums;
using System.Runtime.CompilerServices;

namespace Miniville
{
	class Game
	{
		public Player[] Players = [];
		public int CurrentTurnPlayerIndex;
		public List<Pile> Shop = new();
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

			InitShop();

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
				CardShop();

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

		private void InitShop()
		{
			Shop = [];
			Shop.Add(new Pile().InitStack(Buildings.WHEAT_FIELD));
			Shop.Add(new Pile().InitStack(Buildings.FARM));
			Shop.Add(new Pile().InitStack(Buildings.BAKERY));
			Shop.Add(new Pile().InitStack(Buildings.CAFE));
			Shop.Add(new Pile().InitStack(Buildings.CONVENIENCE_STORE));
			Shop.Add(new Pile().InitStack(Buildings.FOREST));
			Shop.Add(new Pile().InitStack(Buildings.STADIUM));
			Shop.Add(new Pile().InitStack(Buildings.BUSINESS_CENTER));
			Shop.Add(new Pile().InitStack(Buildings.TV_STATION));
			Shop.Add(new Pile().InitStack(Buildings.CHEESE_FACTORY));
			Shop.Add(new Pile().InitStack(Buildings.FURNITURE_FACTORY));
			Shop.Add(new Pile().InitStack(Buildings.MINE));
			Shop.Add(new Pile().InitStack(Buildings.RESTAURANT));
			Shop.Add(new Pile().InitStack(Buildings.ORCHARD));
			Shop.Add(new Pile().InitStack(Buildings.FRUIT_AND_VEGETABLE_MARKET));
    }

		private void CardShop()
		{
			Player player = Players[CurrentTurnPlayerIndex];

			if (player.Type == PlayerType.HUMAN)
			{
				if (!HumanInterface.AskBool("It is shopping time! Would you like to buy a card?")) return;

				Console.WriteLine("you can buy one of these: (enter number)");
				for (int i = 0; i < Shop.Count; i++)
				{
					Pile pile = Shop[i];
					if(pile.IsEmpty())
					{
	          Console.WriteLine($"({i})  --  [{pile.Cards.Count()}] {pile.Top().Color} - {pile.Top().Name} : {pile.Top().Desc} - {pile.Top().Price}$");
	        } else
					{
	          Console.WriteLine($"({i})  --  [0] EMPTY");
					}
				}
				
				int pileIndex = HumanInterface.AskIndex("", Shop.Count + 1) - 1;
				Pile chosenPile = Shop[pileIndex];
				if (chosenPile.IsEmpty())
				{
					Console.WriteLine("you tried to shop an empty pile ._.");
					return;
				}
				player.BuyCard(chosenPile.Pickup());
			}
			else
			{
				int pileIndex = new Random().Next(0,Shop.Count);
				Console.WriteLine($"the bot is shoping the card {pileIndex}");
				Pile chosenPile = Shop[pileIndex];
				if (chosenPile.IsEmpty())
				{
					Console.WriteLine("the bot tried to shop an empty pile ._.");
					return;
				}
				player.BuyCard(chosenPile.Pickup());
			}
		}
	}
}
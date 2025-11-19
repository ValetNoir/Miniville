using Miniville.BuildingStuff;
using Miniville.Enums;
using Miniville.Interfaces;
using System.Runtime.CompilerServices;

namespace Miniville
{
	class Game
	{
		public Player[] Players = [];
		public int CurrentTurnPlayerIndex;
		public List<Pile> Shop;
		private Dice dice = new();
		private int turns = 0;

		public Game(int humanAmount = 1, int botAmount = 1)
		{
			// setup
			List<Player> playersList = [];

			for (int i = 0; i < humanAmount; i++)
			{
				string actualName = (humanAmount > 1) ? $"human {i+1}" : "human";
				playersList.Add(new Player(PlayerType.HUMAN, actualName));
			}

			for (int i = 0; i < botAmount; i++)
			{
				string actualName = (humanAmount > 1) ? $"bot {i+1}" : "bot";
				playersList.Add(new Player(PlayerType.BOT, actualName));
			}

			InitShop();

			Players = [.. playersList];
			
			// play
			Loop();
		}

		public void Loop()
		{
			while (!Ended())
			{
                DisplayPlayerInfo();

                // get the playing player and the others
                turns++;
				CurrentTurnPlayerIndex = turns % Players.Length;
				Player playingPlayer = Players[CurrentTurnPlayerIndex];
				IEnumerable<Player> otherPlayers = Players.Where((val, idx) => idx != CurrentTurnPlayerIndex);

				// roooool the dice yea
				// logic to choose to play with 1 or 2 dices -> argument true for 2 dices
				bool isDoubleDice;
				if (Players[CurrentTurnPlayerIndex].Type == PlayerType.HUMAN)
					isDoubleDice = HumanInterface.AskBool("\nDo you want to roll 2 dices?");
				else
					isDoubleDice = BotInterface.AskBool();
                
				var diceResult = dice.Roll(isDoubleDice);
				Console.WriteLine($"\n{playingPlayer} rolled a {diceResult.total} !");

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
			Shop = new List<Pile>();
			Shop.Add(new Pile(Buildings.WHEAT_FIELD));
			Shop.Add(new Pile(Buildings.RESTAURANT));
            Shop.Add(new Pile(Buildings.CAFE));
            Shop.Add(new Pile(Buildings.FARM));
            Shop.Add(new Pile(Buildings.STADIUM));
            Shop.Add(new Pile(Buildings.BAKERY));
            Shop.Add(new Pile(Buildings.BUSINESS_CENTER));
            Shop.Add(new Pile(Buildings.CHEESE_FACTORY));
            Shop.Add(new Pile(Buildings.CONVENIENCE_STORE));
            Shop.Add(new Pile(Buildings.FOREST));
            Shop.Add(new Pile(Buildings.MINE));
			Shop.Add(new Pile(Buildings.FRUIT_AND_VEGETABLE_MARKET));
			Shop.Add(new Pile(Buildings.FURNITURE_FACTORY));
			Shop.Add(new Pile(Buildings.ORCHARD));
			Shop.Add(new Pile(Buildings.TV_STATION));
        }

		private void CardShop()
		{
			Player player = Players[CurrentTurnPlayerIndex];

            Console.WriteLine($"{player.Name} has {player.Money} coin(s)");

            if (player.Type == PlayerType.HUMAN)
			{
				if (!HumanInterface.AskBool("\nIt is shopping time! Would you like to buy a card?")) return;
				Thread.Sleep(1000);
				
				Console.WriteLine($"\n{player.Name} can buy one of these: (enter number)");
				int cardIndex = 0;
                for (int i = 0; i < Shop.Count; i++)
                {
					if(!Shop[i].IsEmpty())
					{
                        cardIndex++;
						var card = Shop[i].Loop()[0];
                        Console.Write($"({cardIndex})  --  [ ");
                        foreach (var number in card.ActiveNumbers)
                        {
                            Console.Write($"{number} ");
                        }
                        Console.WriteLine($"] {card.Color} - {card.Name} : {card.Desc} - {card.Price}$");
                        Thread.Sleep(100);
                    }
                }
				Console.WriteLine();
				int cardBoughtIndex = HumanInterface.AskIndex("", ++cardIndex, 0);
				player.BuyCard(Shop[--cardBoughtIndex].Pickup());
			}
			else
			{
                if (!BotInterface.AskBool()) return;

                Console.WriteLine($"\n{player.Name} can buy one of these: (enter number)");
                int cardIndex = 0;
                for (int i = 0; i < Shop.Count; i++)
                {
                    if (!Shop[i].IsEmpty())
                    {
                        cardIndex++;
                        var card = Shop[i].Loop()[0];
                        Console.Write($"({cardIndex})  --  [ ");
                        foreach (var number in card.ActiveNumbers)
                        {
                            Console.Write($"{number} ");
                        }
                        Console.WriteLine($"] {card.Color} - {card.Name} : {card.Desc} - {card.Price}$");
                        Thread.Sleep(200);
                    }
                }
                int cardBoughtIndex = BotInterface.AskIndex(cardIndex);
                player.BuyCard(Shop[cardBoughtIndex].Pickup());
            }
		}

		private void DisplayPlayerInfo()
		{
            foreach (Player player in Players)
            {
                Console.WriteLine();
                player.DisplayCards();
				Console.WriteLine($"{player.Name} has {player.Money} coin(s)");
				Thread.Sleep(1000);
            }
        }
	}
}
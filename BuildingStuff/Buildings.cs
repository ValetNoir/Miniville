using Miniville.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville.BuildingStuff
{

    internal class Buildings
    {

        static public readonly Card WHEAT_FIELD = new Card(
            new int[] { 1 },
            CardColor.BLUE,
            CardType.WHEAT,
            "Wheat Field",
            "Get 1 coin from the bank. Everyone can use this card on anyone's turn.",
            1,
            EffectsGenerator.Instance.GenerateMoney(1)
        );

        static public readonly Card FARM = new Card(
            new int[] { 2 },
            CardColor.BLUE,
            CardType.COW,
            "Farm",
            "Get 1 coin from the bank. Everyone can use this card on anyone's turn.",
            1,
            EffectsGenerator.Instance.GenerateMoney(1)
        );

        static public readonly Card BAKERY = new Card(
            new int[] { 2, 3 },
            CardColor.GREEN,
            CardType.SHOP,
            "Bakery",
            "Get 1 coin from the bank. Only on your turn.",
            1,
            EffectsGenerator.Instance.GenerateMoney(1)
        );

        static public readonly Card CAFE = new Card(
            new int[] { 3 },
            CardColor.RED,
            CardType.CUP,
            "Cafe",
            "Get 1 coin from the player who rolled the dice. Only on their turn.",
            2,
            EffectsGenerator.Instance.StealMoneyFromCurrentPlayer(1)
        );

        static public readonly Card CONVENIENCE_STORE = new Card(
            new int[] { 4 },
            CardColor.GREEN,
            CardType.SHOP,
            "Convenience Store",
            "Get 3 coins from the bank. Only on your turn.",
            2,
            EffectsGenerator.Instance.GenerateMoney(3)
        );

        static public readonly Card FOREST = new Card(
            new int[] { 5 },
            CardColor.BLUE,
            CardType.GEAR,
            "Forest",
            "Get 1 coin from the bank. Everyone can use this card on anyone's turn.",
            3,
            EffectsGenerator.Instance.GenerateMoney(1)
        );

        static public readonly Card STADIUM = new Card(
            new int[] { 6 },
            CardColor.PURPLE,
            CardType.BOMB,
            "Stadium",
            "Get 2 coins from all players. Only on your turn.",
            6,
            EffectsGenerator.Instance.StealMoneyFromAll(2)
        );

        static public readonly Card BUSINESS_CENTER = new Card(
            new int[] { 6 },
            CardColor.PURPLE,
            CardType.BOMB,
            "Business Center",
            "Trade one of your buildings with another player. Only on your turn.",
            8,
            EffectsGenerator.Instance.TradeBuilding()
        );

        static public readonly Card TV_STATION = new Card(
            new int[] { 6 },
            CardColor.PURPLE,
            CardType.BOMB,
            "TV Station",
            "Choose a player to give you 5 coins. Only on your turn.",
            7,
            EffectsGenerator.Instance.StealMoneyFromChosen(5)
        );

        static public readonly Card CHEESE_FACTORY = new Card(
            new int[] { 7 },
            CardColor.GREEN,
            CardType.FACTORY,
            "Cheese Factory",
            "Get 3 coins from the bank for each COW building you own. Only on your turn.",
            5,
            EffectsGenerator.Instance.GenerateMoneyPerType(3, CardType.COW)
        );

        static public readonly Card FURNITURE_FACTORY = new Card(
            new int[] { 8 },
            CardColor.GREEN,
            CardType.FACTORY,
            "Furniture Factory",
            "Get 3 coins from the bank for each GEAR building you own. Only on your turn.",
            3,
            EffectsGenerator.Instance.GenerateMoneyPerType(3, CardType.GEAR)
        );

        static public readonly Card MINE = new Card(
            new int[] { 9 },
            CardColor.BLUE,
            CardType.GEAR,
            "Mine",
            "Get 5 coins from the bank. Everyone can use this card on anyone's turn.",
            6,
            EffectsGenerator.Instance.GenerateMoney(5)
        );

        static public readonly Card RESTAURANT = new Card(
            new int[] { 9, 10 },
            CardColor.RED,
            CardType.CUP,
            "Restaurant",
            "Get 2 coins from the player who rolled the dice. Only on their turn.",
            3,
            EffectsGenerator.Instance.StealMoneyFromCurrentPlayer(2)
        );

        static public readonly Card ORCHARD = new Card(
            new int[] { 10 },
            CardColor.BLUE,
            CardType.WHEAT,
            "Orchard",
            "Get 3 coins from the bank. Everyone can use this card on anyone's turn.",
            3,
            EffectsGenerator.Instance.GenerateMoney(3)
        );

        static public readonly Card FRUIT_AND_VEGETABLE_MARKET = new Card(
            new int[] { 11, 12 },
            CardColor.GREEN,
            CardType.SHOP,
            "Fruit and Vegetable Market",
            "Get 2 coins from the bank for each WHEAT building you own. Only on your turn.",
            2,
            EffectsGenerator.Instance.GenerateMoneyPerType(2, CardType.WHEAT)
        );
    }
}

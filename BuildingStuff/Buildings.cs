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

        public readonly Card WHEAT_FIELD = new Card(
            new int[] { 1 },
            CardColor.BLUE,
            CardType.WHEAT,
            "Wheat Field",
            "Get 1 coin from the bank. Everyone can use this card on anyone's turn.",
            1,
            EffectsGenerator.Instance.GenerateMoney(1)
        );

        public readonly Card BAKERY = new Card(
            new int[] { 2, 3 },
            CardColor.GREEN,
            CardType.SHOP,
            "Bakery",
            "Get 1 coin from the bank. Only on your turn.",
            1,
            EffectsGenerator.Instance.GenerateMoney(1)
        );

        public readonly Card CAFE = new Card(
            new int[] { 3 },
            CardColor.RED,
            CardType.CUP,
            "Cafe",
            "Get 1 coin from the player who rolled the dice. Only on their turn.",
            2,
            EffectsGenerator.Instance.StealMoney(1)
        );

        public readonly Card CONVENIENCE_STORE = new Card(
            new int[] { 4 },
            CardColor.GREEN,
            CardType.SHOP,
            "Convenience Store",
            "Get 3 coins from the bank. Only on your turn.",
            2,
            EffectsGenerator.Instance.GenerateMoney(3)
        );

        public readonly Card FOREST = new Card(
            new int[] { 5 },
            CardColor.BLUE,
            CardType.GEAR,
            "Forest",
            "Get 1 coin from the bank. Everyone can use this card on anyone's turn.",
            3,
            EffectsGenerator.Instance.GenerateMoney(1)
        );
    }
}

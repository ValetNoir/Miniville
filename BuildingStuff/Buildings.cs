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
            CardType.FIELD,
            "Wheat Field",
            "Get 1 coin from the bank. Everyone can use this card on anyone's turn.",
            1,
            EffectsGenerator.Instance.GenerateMoney(1)
        );
    }
}

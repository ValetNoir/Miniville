using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class Buildings
    {
        
        private Func<Player, Player, bool> generateMoney(int amount)
        {
            return (Player receiver, Player giver = null) =>
            {
                receiver.Money += amount;

                return true;
            };
        }

        private Func<Player, Player, bool> stealMoney(int amount)
        {
            return (Player receiver, Player giver) =>
            {
                if (giver.Money < amount)
                {
                    amount = giver.Money;
                }
                giver.Money -= amount;
                receiver.Money += amount;

                return true;
            };
        }

        private Func<Player, Player, bool> generateMoneyWithType(int amount, CardType type)
        {
            return (Player receiver, Player giver = null) =>
            {
                int count = receiver.Cards.Where(card => card.Type == type).Count();
                receiver.Money += amount * count;

                return true;
            };
        }

        public readonly Card WHEAT_FIELD = new Card(
            new int[] { 1 },
            CardColor.BLUE,
            CardType.RAW_MATERIAL,
            "Wheat Field",
            "Get 1 coin from the bank. Everyone can use this card on anyone's turn.",
            1,
            generateMoney(1)
        );
    }
}

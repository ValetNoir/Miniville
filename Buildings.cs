using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class EffectsGenerator
    {
        public static readonly EffectsGenerator Instance = new EffectsGenerator();
        public EffectsGenerator() { }

        public Func<Player, Player, bool> GenerateMoney(int amount)
        {
            return (Player receiver, Player giver = null) =>
            {
                receiver.Money += amount;

                return true;
            };
        }

        public Func<Player, Player, bool> StealMoney(int amount)
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

        public Func<Player, Player, bool> GenerateMoneyWithType(int amount, CardType type)
        {
            return (Player receiver, Player giver = null) =>
            {
                int count = receiver.Cards.Where(card => card.Type == type).Count();
                receiver.Money += amount * count;

                return true;
            };
        }
    }

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

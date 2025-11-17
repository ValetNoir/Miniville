using Miniville.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville.BuildingStuff
{
    internal class EffectsGenerator
    {
        public static readonly EffectsGenerator Instance = new EffectsGenerator();
        public EffectsGenerator() { }

        public Func<Player, Player, bool> GenerateMoney(int amount)
        {
            return (receiver, _giver) =>
            {
                receiver.Money += amount;

                return true;
            };
        }

        public Func<Player, Player, bool> StealMoney(int amount)
        {
            return (receiver, giver) =>
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
            return (receiver, _giver) =>
            {
                int count = receiver.Cards.Where(card => card.Type == type).Count();
                receiver.Money += amount * count;

                return true;
            };
        }
    }
}

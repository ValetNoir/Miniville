using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    public class Dice
    {
        private Random rdm = new Random();

        public (int[] faces, int total) Roll(bool multiDice = false)
        {
            int resultD1 = rdm.Next(1,7);
            if (multiDice)
            {
                int resultD2 = rdm.Next(1,7);
                int total = resultD1 + resultD2;
                int[] result = { resultD1, resultD2 };
                return (result, total);
            }
            else
            {
                int[] result = { resultD1 };
                return (result, resultD1);
            }
        }
    }
}

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

        public int Roll(bool multiDice = false)
        {
            int resultD1 = rdm.Next(7);
            if (multiDice)
            {
                int resultD2 = rdm.Next(7);
                int total = resultD1 + resultD2;
                Console.WriteLine("Dices result : {0}, {1}  Total : {2}", resultD1, resultD2, total);
                return total;
            }
            else
            {
                Console.WriteLine($"Dice result : {resultD1}");
                return resultD1;
            }
        }
    }
}

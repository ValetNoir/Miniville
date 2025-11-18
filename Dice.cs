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
            int result = rdm.Next(7);
            if (multiDice) { result += rdm.Next(7); }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville.Interfaces
{
    internal static class BotInterface
    {
        private static Random random = new Random();

        public static int AskIndex(int arrayLength, int excludedIndex = -1)
        {
            var exclude = new HashSet<int>() { excludedIndex };
            var range = Enumerable.Range(1, 100).Where(i => !exclude.Contains(i));

            int index = random.Next(0, 100 - exclude.Count);
            return range.ElementAt(index);
        }
        public static bool AskBool()
        {
            Random random = new Random();
            return random.Next(0, 1) == 0;
        }
    }
}

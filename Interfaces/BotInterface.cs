using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Miniville.Interfaces
{
    internal static class BotInterface
    {
        private static Random random = new Random();

        public static int AskIndex(int arrayLength, int excludedIndex = -1)
        {
            while (true)
            {
                int chosenIndex = random.Next(0, arrayLength);
                if (chosenIndex != excludedIndex)
                    return chosenIndex;
            }
        }
        public static bool AskBool()
        {
            Random random = new Random();
            return random.Next(0, 1) == 0;
        }
    }
}

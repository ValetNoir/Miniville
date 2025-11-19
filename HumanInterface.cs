using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal static class HumanInterface
    {
        public static int AskIndex(string text, int arrayLength, int excludedIndex = -1)
        {
            while (true)
            {
                Console.Write(text);
                var input = Console.ReadLine();
                if (int.TryParse(input, out int chosenIndex) && chosenIndex >= 0 && chosenIndex < arrayLength)
                {
                    if (chosenIndex != excludedIndex)
                        return chosenIndex;
                    else
                        Console.WriteLine("Invalid choice. Please choose something else.");
                }
                else
                    Console.WriteLine("Invalid input. Please enter a valid player number.");
            }
        }

        public static bool AskBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (y/n): ");
                var input = Console.ReadLine().ToLower();
                if (input == "y")
                    return true;
                else if (input == "n")
                    return false;
                else
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }
    }
}

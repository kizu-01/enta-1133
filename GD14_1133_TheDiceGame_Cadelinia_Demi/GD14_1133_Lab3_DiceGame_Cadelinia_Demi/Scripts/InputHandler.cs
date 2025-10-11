using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal static class InputHandler
    {
        /// <summary>
        /// Ask yes/no and return bool. Keeps looping until user types yes or no
        /// Displays ">> " prompt and accepts yes/no
        /// If message is provided, it prints it first
        /// </summary>
        public static bool GetYesNo(string message)
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(message))
                    Console.WriteLine(message);
                Console.Write(">> ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "yes" || input == "y") return true;
                if (input == "no" || input == "n") return false;

                Console.Clear();
                Console.WriteLine("Choose Yes or No answers only (type yes or no).");
            }
        }
    }
}
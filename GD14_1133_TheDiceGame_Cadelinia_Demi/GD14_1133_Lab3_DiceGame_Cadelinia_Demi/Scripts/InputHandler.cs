using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class InputHandler
    {
        /// <summary>
        /// Gets a Yes/No response with retry loop
        /// </summary>
        public static bool GetYesNo(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                Console.Write(">> ");
                string input = Console.ReadLine().ToLower();

                if (input == "yes") return true;
                if (input == "no") return false;

                Console.Clear();
                Console.WriteLine("Yes or no answers only...\n");
            }
        }
    }
}

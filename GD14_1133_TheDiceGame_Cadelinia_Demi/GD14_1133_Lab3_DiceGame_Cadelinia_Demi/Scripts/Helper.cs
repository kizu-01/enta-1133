using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal static class Helper
    {
        /// <summary>
        /// Typewriter effect
        /// </summary>
        public static void Typewrite(string text, int delay = 12)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Pause until Enter for clean prompt
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("- Press Enter to continue -");
            Console.ReadLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Helper
    {
        /// <summary>
        /// Prints text with typewriter effect for smooth transitions
        /// </summary>
        public static void Typewrite(string text, int delay = 15)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Clean "Press Enter to continue" prompt
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine("- Press Enter to continue -");
            Console.ReadLine();
        }
    }
}

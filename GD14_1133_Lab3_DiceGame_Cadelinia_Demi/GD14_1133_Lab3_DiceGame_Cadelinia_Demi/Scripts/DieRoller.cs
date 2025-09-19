using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class DieRoller
    {
        private Random rd = new Random();

        // Roll a die with the given sides
        internal int Roll(int sides)
        {
            return rd.Next(1, sides + 1);
        }

        // Roll the dice and return the total
        internal int RollAll()
        {
            int d6 = Roll(6);
            int d8 = Roll(8);
            int d12 = Roll(12);
            int d20 = Roll(20);

            // Created input for each dice rolled
            Console.WriteLine($"D6 Rolled: {d6}");
            Console.WriteLine($"D8 Rolled: {d8}");
            Console.WriteLine($"D12 Rolled: {d12}");
            Console.WriteLine($"D20 Rolled: {d20}");

            // Return total
            return d6 + d8 + d12 + d20;
        }
    }
}

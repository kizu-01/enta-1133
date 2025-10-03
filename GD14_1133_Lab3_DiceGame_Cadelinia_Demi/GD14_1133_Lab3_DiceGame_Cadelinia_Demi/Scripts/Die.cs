using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Die
    {
        private Random rd = new Random();

        // Roll a die with the given sides
        internal int Roll(int sides)
        {
            return rd.Next(1, sides + 1);
        }
    }
}

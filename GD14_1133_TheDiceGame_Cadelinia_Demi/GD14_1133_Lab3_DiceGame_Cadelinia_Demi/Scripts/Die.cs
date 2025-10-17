using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Die
    {
        private Random rnd = new Random();

        // Roll a die with the given sides
        public int Roll(int sides)
        {
            if (sides < 1) throw new ArgumentException("Die must have at least one side.");
            return rnd.Next(1, sides + 1);
        }
    }
}
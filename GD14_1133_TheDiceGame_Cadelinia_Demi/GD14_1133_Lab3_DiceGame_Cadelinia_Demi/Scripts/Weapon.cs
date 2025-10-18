using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Weapon : Item
    {
        private int diceCount;
        private int diceSides;

        // Example: 1d7 means diceCount=1, diceSides=7
        public Weapon(string name, int diceCount, int diceSides)
        {
            DisplayName = name;
            this.diceCount = diceCount;
            this.diceSides = diceSides;
        }

        public int DiceCount => diceCount;
        public int DiceSides => diceSides;

        public override int Use(Die die)
        {
            int total = 0;
            for (int i = 0; i < diceCount; i++)
            {
                total += die.Roll(diceSides);
            }
            return total;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Consumable : Item
    {
        private int diceCount;
        private int diceSides;

        // Consumable gives healing amount (2d7 etc.)
        public Consumable(string name, int diceCount, int diceSides)
        {
            DisplayName = name;
            this.diceCount = diceCount;
            this.diceSides = diceSides;
        }

        public override bool IsConsumable => true;

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
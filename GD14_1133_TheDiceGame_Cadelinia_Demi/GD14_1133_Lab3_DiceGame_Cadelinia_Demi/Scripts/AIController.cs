using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class AIController
    {
        private Random rd = new Random();

        /// <summary>
        /// AI chooses a random die from available dice
        /// </summary>
        public int ChooseDie(List<int> availableDice)
        {
            int index = rd.Next(availableDice.Count);
            return availableDice[index];
        }

        /// <summary>
        /// AI chooses win condition: higher or lower
        /// </summary>
        public string ChooseCondition()
        {
            return rd.Next(2) == 0 ? "higher" : "lower";
        }
    }
}

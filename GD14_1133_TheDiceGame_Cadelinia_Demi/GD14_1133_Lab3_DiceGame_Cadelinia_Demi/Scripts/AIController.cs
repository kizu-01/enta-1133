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

        public int ChooseDie(List<int> availableDice)
        {
            if (availableDice == null || availableDice.Count == 0)
                throw new InvalidOperationException("AI has no dice available.");

            int idx = rd.Next(availableDice.Count);
            int chosen = availableDice[idx];
            Helper.Typewrite($"Opponent chooses a d{chosen}...");
            return chosen;
        }

        public string ChooseCondition()
        {
            return rd.Next(2) == 0 ? "higher" : "lower";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Turn
    {
        private Player player;
        private Die dice;

        public Turn(Player player, Die dice)
        {
            this.player = player;
            this.dice = dice;
        }

        /// <summary>
        /// Do a roll for a chosen die and pause until Enter is pressed.
        /// </summary>
        public int DoTurn(int chosenSides)
        {
            Console.WriteLine();
            Helper.Typewrite($"{player.Name} steps up to roll the {chosenSides}-sided die...");
            int roll = dice.Roll(chosenSides);
            Helper.Typewrite($"And rolls a {roll}!");
            Console.WriteLine();
            Helper.Pause();
            return roll;
        }
    }
}
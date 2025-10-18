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
        /// Used to roll a weapon (or consumable) and show message. Returns displayed result.
        /// </summary>
        public int DoUse(Item item)
        {
            System.Console.WriteLine();
            Helper.Typewrite($"{player.Name} uses {item.DisplayName}...");
            int result = item.Use(dice);
            if (item is Weapon)
            {
                Helper.Typewrite($"It deals {result} damage!");
            }
            else
            {
                Helper.Typewrite($"It restores {result} HP!");
            }
            System.Console.WriteLine();
            Helper.Pause();
            return result;
        }

        /// <summary>
        /// Raw-use method that performs the roll but doesn't print big messages
        /// good for simultaneous roll/compare pattern. Still returns result.
        /// </summary>
        public int DoUseRaw(Item item)
        {
            int result = item.Use(dice);
            return result;
        }
    }
}
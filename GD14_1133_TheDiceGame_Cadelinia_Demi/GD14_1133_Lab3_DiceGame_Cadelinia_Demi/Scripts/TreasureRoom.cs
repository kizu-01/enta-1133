using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class TreasureRoom : Room
    {
        private int dieValue;
        private bool taken = false;

        public TreasureRoom(string name, int dieValue) : base(name)
        {
            this.dieValue = dieValue;
        }

        public override string RoomDescription()
        {
            return "This room oozes mysterious enchantment. You sense a lucky die here.";
        }

        public override void OnSearchedRoom(Player player)
        {
            Console.WriteLine();
            if (!taken)
            {
                if (!player.DicePoolCopy.Contains(dieValue))
                {
                    Helper.Typewrite($"You found a d{dieValue} die!");
                    player.AddDie(dieValue);
                }
                else
                {
                    Helper.Typewrite($"You already obtained a d{dieValue} die here.");
                }
                taken = true;
            }
            else
            {
                Helper.Typewrite("You already searched this room. Nothing of importance remains.");
            }
            Helper.Pause();
        }

        public override void OnEnteredRoom()
        {
            Console.WriteLine();
            if (!Visited)
            {
                Helper.Typewrite($"You entered {Name}. This room oozes mysterious enchantment. You sense a lucky die here.");
                Visited = true;
            }
            else
            {
                Helper.Typewrite($"You entered {Name}. You've visited this place before.");
            }
        }
    }
}
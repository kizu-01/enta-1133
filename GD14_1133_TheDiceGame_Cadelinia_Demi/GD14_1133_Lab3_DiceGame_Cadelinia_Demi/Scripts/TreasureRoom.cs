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
        private string weaponName;

        // Accept a name and also the die sides and display weapon name
        public TreasureRoom(string name, int dieValue, string weaponName) : base(name)
        {
            this.dieValue = dieValue;
            this.weaponName = weaponName;
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
                // create a Weapon item based on dieValue
                if (!player.GetWeapons().Any(w => w.DiceSides == dieValue))
                {
                    // choose a die of dieValue for weapons (1d7,1d12,1d21)
                    var weapon = new Weapon(weaponName, 1, dieValue);
                    Helper.Typewrite($"You found a {weapon.DisplayName}!");
                    player.AddItem(weapon);
                }
                else
                {
                    Helper.Typewrite($"You already obtained a d{dieValue} weapon here.");
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
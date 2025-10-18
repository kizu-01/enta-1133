using GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class HealRoom : Room
    {
        private bool taken = false;

        public HealRoom(string name) : base(name) { }

        public override void OnSearchedRoom(Player player)
        {
            Console.WriteLine();
            if (!taken)
            {
                // Give one potion (randomly gives small or large for variability)
                var rnd = new Random();
                Consumable potion;
                if (rnd.Next(100) < 60)
                    potion = new Consumable("Small Healing Potion (3d7)", 3, 7);
                else
                    potion = new Consumable("Big Healing Potion (3d12)", 3, 12);

                player.AddItem(potion);
                Helper.Typewrite($"You found a {potion.DisplayName}!");
                taken = true;
            }
            else
            {
                Helper.Typewrite("You already searched this sanctuary. Nothing more to find.");
            }
            Helper.Pause();
        }

        public override string RoomDescription()
        {
            return "You sense an unknown ambience here…";
        }

        public override void OnEnteredRoom()
        {
            Console.WriteLine(RoomDescription());
        }

        // Called by GameManager after a victory to allow new potions to appear
        public void ResetTaken()
        {
            taken = false;
            Visited = false;
        }
    }
}
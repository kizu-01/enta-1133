using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class EncounterRoom : Room
    {
        private GameManager gm;
        private bool defeated = false;

        public EncounterRoom(string name, GameManager gm) : base(name)
        {
            this.gm = gm;
        }

        public override string RoomDescription()
        {
            return "You feel immense malice in the air. An opponent awaits...";
        }

        public override void OnSearchedRoom(Player player)
        {
            Console.WriteLine();

            // Prevent re-battle
            if (defeated)
            {
                Helper.Typewrite("The place stays silent. The opponent has already been defeated.");
                Helper.Pause();
                return;
            }

            // If player has no weapons, they cannot fight
            if (!player.HasWeapons())
            {
                Helper.Typewrite("You have no weapons. You cannot enter battle!");
                Helper.Pause();
                return;
            }

            // Ask player if they want to battle
            if (!InputHandler.GetYesNo("A wild opponent appeared! Do you wish to battle? [Yes/No]"))
            {
                Helper.Typewrite("You decided to retreat for now.");
                return;
            }

            // Let GameManager handle the battle
            bool playerWon = gm.Battle(new Player("Opponent", 50));

            if (playerWon)
            {
                Helper.Typewrite("You defeated the opponent! The room grows calm...");
                defeated = true;
            }
            else
            {
                Helper.Typewrite("You were defeated... The Chance frowns upon your fate.");
            }

            // Pause to better see the outcome
            Helper.Pause();
        }
    }
}
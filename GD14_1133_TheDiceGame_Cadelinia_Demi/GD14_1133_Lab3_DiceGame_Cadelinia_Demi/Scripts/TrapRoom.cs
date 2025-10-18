using GD14_1133_Lab3_DiceGame_Cadelinia_Demi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class TrapRoom : Room
    {
        public TrapRoom(string name) : base(name) { }

        public override void OnSearchedRoom(Player player)
        {
            Helper.Typewrite("A hidden trap activates! You take some damage...");
            // deal damage (1d7)
            Die die = new Die();
            int dmg = die.Roll(7);
            player.TakeDamage(dmg);
            Helper.Typewrite($"You took {dmg} damage. Current HP: {player.HP}/{player.MaxHP}");
            Helper.Pause();
        }

        public override string RoomDescription()
        {
            return "You sense an unknown ambience here…";
        }

        public override void OnEnteredRoom()
        {
            System.Console.WriteLine(RoomDescription());
        }
    }
}
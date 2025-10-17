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
        public HealRoom(string name) : base(name) { }

        public override void OnSearchedRoom(Player player)
        {
            Helper.Typewrite("You feel rejuvenated! Perhaps your luck with dice will improve...");
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
    }
}
using GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    // Hollow Room (Spawn point)
    internal class EmptyRoom : Room
    {
        public EmptyRoom(string name) : base(name) { }

        public override void OnEnteredRoom()
        {
            Helper.Typewrite($"You enter {Name}. {RoomDescription()}");
        }

        public override void OnSearchedRoom(Player player)
        {
            Helper.Typewrite("Nothing of interest here...");
            Helper.Pause();
        }

        public override string RoomDescription()
        {
            return "It's filled with nothing but emptiness.";
        }
    }
}
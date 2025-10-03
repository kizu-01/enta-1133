using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Player
    {
        public string Name { get; private set; }
        public int CurrentRoll { get; set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}

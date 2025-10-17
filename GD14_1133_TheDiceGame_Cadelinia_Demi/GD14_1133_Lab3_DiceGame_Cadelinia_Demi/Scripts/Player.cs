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
        public int Score { get; private set; }
        public List<int> DicePool { get; private set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
            DicePool = new List<int> { 7, 12, 21 };
        }

        // Reset score and dice pool
        public void ResetPlayer()
        {
            Score = 0;
            DicePool = new List<int> { 7, 12, 21 };
        }

        // Add a point
        public void AddPoint()
        {
            Score++;
        }
    }
}

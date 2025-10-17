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

        // Dice pool
        public List<int> DicePool { get; private set; } = new List<int>();

        // For display and selection
        public List<int> DicePoolCopy => new List<int>(DicePool);

        public int DicePoolCount => DicePool.Count;

        // Score for battle rounds
        public int Score { get; private set; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        // Add a die to the pool
        public void AddDie(int sides)
        {
            DicePool.Add(sides);
        }

        // Remove a die from the pool (after rolling)
        public void RemoveDie(int sides)
        {
            DicePool.Remove(sides);
        }

        // Backup dice pool (used for tie-breaker restore)
        public void RestoreDicePool(List<int> backup)
        {
            DicePool = new List<int>(backup);
        }

        // Check if player has at least one die
        public bool HasDice()
        {
            return DicePool.Count > 0;
        }

        // Score handling
        public void AddPoint()
        {
            Score++;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        // Reset the player completely
        public void ResetPlayer(bool resetDice)
        {
            Score = 0;
            if (resetDice)
            {
                DicePool.Clear();
            }
        }
    }
}
using GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();

            // Show intro + ask player name + instructions once
            gm.ShowIntroAndInstructions();

            // Start dungeon exploration (will loop until player chooses to exit or play again after defeat)
            gm.StartDungeon();
        }
    }
}
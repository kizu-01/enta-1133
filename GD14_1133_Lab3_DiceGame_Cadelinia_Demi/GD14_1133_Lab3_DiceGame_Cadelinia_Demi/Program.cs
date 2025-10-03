using GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();

            while (true)
            {
                Console.Clear(); // Clear before starting each game
                game.Play();

                // Ask if the player wants to play again
                if (!InputHandler.GetYesNo("Do you want to play again? [Yes/No]"))
                {
                    Console.WriteLine();
                    Console.WriteLine("Until then, mortal. Farewell!");
                    break;
                }
            }
        }
    }
}

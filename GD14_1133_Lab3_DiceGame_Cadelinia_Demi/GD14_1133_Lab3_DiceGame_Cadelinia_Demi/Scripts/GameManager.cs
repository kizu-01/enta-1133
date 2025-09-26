using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class GameManager
    {
        private Player player;
        private Player computer;
        private Die dice = new Die();
        private Random rd = new Random();
        public void Play()
        {
            if (!Intro())
                return;

            Instructions();
            Game();
            Outro();
        }
        // Show welcome message
        private bool Intro()
        {
            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine("Welcome to The Dice Game! Made by Demi Cadelinia as of September 25, 2025.");
            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine();

            Console.WriteLine("Please enter your name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName);
            computer = new Player("Opponent");
            Console.WriteLine();

            Console.WriteLine("Do you want to play? [Yes/No]");
            string answer = Console.ReadLine().ToLower();
            Console.WriteLine();

            if (answer == "yes")
            {
                Console.WriteLine("Great, Let's Play!");
                return true;
            }
            else if (answer == "no")
            {
                Console.WriteLine("Thanks for your time. Goodbye!");
                return false;
            }
            else
            {
                Console.WriteLine("That's alright! You don't have to play. Exiting game...");
                return false;
            }
        }
        // Show game instructions
        private void Instructions()
        {
            Console.WriteLine();
            Console.WriteLine("How to Play:");
            Console.WriteLine("1. You and your opponent will each roll a dice.");
            Console.WriteLine("2. You get to choose which die to roll (d4, d6, d8, d12, or d20).");
            Console.WriteLine("3. The opponent will automatically choose a die.");
            Console.WriteLine("4. Whoever rolls the higher number wins the round and gets a point.");
            Console.WriteLine("5. If it’s a tie, no one gets a point and the game ends.");
        }
        // Dice rolls for 4 multiple dice
        private void Game()
        {
            Console.WriteLine();
            Console.WriteLine("That's it. Let the game begin!");
            Console.WriteLine();
            Console.WriteLine("- Press any key to decide turn order -");
            Console.WriteLine();
            Console.ReadKey();

            // Decide turn order
            bool playerFirst = rd.Next(0, 2) == 0;

            int playerScore, computerScore;
            int playerChosenSides = 0, computerChosenSides = 0;

            if (playerFirst)
            {
                Console.WriteLine($"{player.Name} goes first!");
                playerScore = PlayerTurn(20, out playerChosenSides);
                computerScore = ComputerTurn(playerChosenSides, out computerChosenSides);
            }
            else
            {
                Console.WriteLine("Opponent goes first!");
                computerScore = ComputerTurn(20, out computerChosenSides);
                playerScore = PlayerTurn(computerChosenSides, out playerChosenSides);
            }

            Console.WriteLine();

            if (playerScore > computerScore)
            {
                Console.WriteLine($"{player.Name} Wins!");
                player.Score++;
            }
            else if (computerScore > playerScore)
            {
                Console.WriteLine($"{computer.Name} Wins!");
                computer.Score++;
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
            Console.WriteLine();
            Console.WriteLine($"Total Score: {player.Name} = {player.Score} | {computer.Name} = {computer.Score}");
            Console.WriteLine();
        }
        private int PlayerTurn(int maxSides, out int chosenSides)
        {
            Console.WriteLine();
            Console.WriteLine($"{player.Name} Your Turn!");
            Console.WriteLine("Choose your die: d4, d6, d8, d12, d20");
            string choice = Console.ReadLine();

            int sides;

            if (choice == "d4" && 4 <= maxSides) sides = 4;
            else if (choice == "d6" && 6 <= maxSides) sides = 6;
            else if (choice == "d8" && 8 <= maxSides) sides = 8;
            else if (choice == "d12" && 12 <= maxSides) sides = 12;
            else if (choice == "d20" && 20 <= maxSides) sides = 20;
            else
            {
                Console.WriteLine($"Invalid input or number too high! You get a d{maxSides} by default.");
                sides = maxSides;
            }

            chosenSides = sides; // Assigning out parameter

            int roll = dice.Roll(sides);
            Console.WriteLine($"You rolled a {roll} on a d{sides}!");
            Console.WriteLine();
            Console.WriteLine("- Press any key to continue -");
            Console.ReadKey();
            return roll;
        }
        private int ComputerTurn(int maxSides, out int chosenSides)
        {
            Console.WriteLine();
            Console.WriteLine("Opponent’s Turn!");

            int[] diceOptions = { 4, 6, 8, 12, 20 };
            int[] allowedDice = diceOptions.Where(d => d <= maxSides).ToArray();

            int sides = allowedDice[rd.Next(allowedDice.Length)];

            chosenSides = sides;

            int roll = dice.Roll(sides);
            Console.WriteLine($"Opponent chose d{sides} and rolled a {roll}!");
            Console.WriteLine();
            Console.WriteLine("- Press any key to continue -");
            Console.ReadKey();
            return roll;
        }
        private void Outro()
        {
            // Goodbye message
            Console.WriteLine("Goodbye! Thanks for playing :D");
        }
    }
}
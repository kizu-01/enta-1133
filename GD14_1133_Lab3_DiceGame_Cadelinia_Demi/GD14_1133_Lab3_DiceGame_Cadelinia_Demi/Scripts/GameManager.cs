using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class GameManager
    {
        public void Play()
        {
            Intro();
            Game();
            OperatorExamples();
            Outro();
        }
        
        // Show welcome message
        private void Intro()
        {
            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine("Welcome to The Dice Game! This is made by Demi Cadelinia as of September 18, 2025.");
            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine();

        }
        
        // Dice rolls for 4 multiple dice
        private void Game()
        {     
            DieRoller dice = new DieRoller();

            Console.WriteLine("Let's Roll!");
            Console.WriteLine();

            int total = dice.RollAll();

            // Add the overall sum of rolls
            Console.WriteLine($"Total Score: {total}");
            Console.WriteLine();
            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine();
        }
        
        // Explain arithmetic operators with examples
        private void OperatorExamples()
        {
            // Generate random numbers for operator examples
            Random rd = new Random();

            int a = rd.Next(1, 11);
            int b = rd.Next(1, 11);
            
            int c = a;  // For increment example value
            int d = b;  // For decrement example value
            float e = a;    // For division to get decimal answers
            float f = b;

            Console.WriteLine("Arithmetic Operators in C#");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Addition
            Console.WriteLine("+ (Addition): adds the value of numbers");
            Console.WriteLine($"Example: {a} + {b} = {a + b}");
            Console.WriteLine();

            // Subtraction
            Console.WriteLine("- (Subtraction): deducts the value of numbers");
            Console.WriteLine($"Example: {a} - {b} = {a - b}");
            Console.WriteLine();

            // Division
            Console.WriteLine("/ (Division): divides the value of numbers");
            Console.WriteLine($"Example: {e} / {f} = {e / f}");
            Console.WriteLine();

            // Multiplication
            Console.WriteLine("* (Multiplication): multiplies the value of numbers");
            Console.WriteLine($"Example: {a} * {b} = {a * b}");
            Console.WriteLine();

            // Increment
            Console.WriteLine("++ (Increment): adds the value of the number by 1");
            Console.WriteLine($"Example: initial value = {c}, after ++ = {c++}, then final value = {c}");
            Console.WriteLine();

            // Decrement
            Console.WriteLine("-- (Decrement): deducts the value of the number by 1");
            Console.WriteLine($"Example: initial value = {d}, after -- = {d--}, then final value = {d}");
            Console.WriteLine();

            // Modulus
            Console.WriteLine("% (Modulus): gives the remainder of the value of numbers");
            Console.WriteLine($"Example: {a} % {b} = {a % b}");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        private void Outro()
        {
            // Goodbye message
            Console.WriteLine("Goodbye! Thanks for playing :D");
        }
    }
}
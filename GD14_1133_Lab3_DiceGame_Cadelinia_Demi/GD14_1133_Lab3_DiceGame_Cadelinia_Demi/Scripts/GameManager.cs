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
            // Welcome message
            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine();

            Console.WriteLine("Welcome to The Dice Game! This is made by Demi Cadelinia as of September 17, 2025.");
            Console.WriteLine();

            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine();

            // Dice rolls
            DieRoller dice = new DieRoller();
            
            int d6 = dice.Roll(6);
            int d8 = dice.Roll(8);
            int d12 = dice.Roll(12);
            int d20 = dice.Roll(20);

            Console.WriteLine("Let's Roll!");
            Console.WriteLine();

            Console.WriteLine($"D6 Rolled: {d6}");
            Console.WriteLine($"D8 Rolled: {d8}");
            Console.WriteLine($"D12 Rolled: {d12}");
            Console.WriteLine($"D20 Rolled: {d20}");

            // Add all the rolls
            int total = d6 + d8 + d12 + d20;
            Console.WriteLine();
            Console.WriteLine($"Total Score: {total}");
            Console.WriteLine();

            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine();

            // Generate random numbers for operator examples
            Random rand = new Random();

            int a = rand.Next(1, 11);
            int b = rand.Next(1, 11);
            
            // For increment
            int c = a;

            // For decrement
            int d = b;

            // For division to get decimal answers
            float e = a;
            float f = b;

            // Explain arithmetic operators with examples
            Console.WriteLine("Arithmetic Operators in C#");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Addition
            Console.WriteLine("+ (Addition): adds the value of numbers");
            Console.WriteLine();
            Console.WriteLine($"Example: {a} + {b} = {a + b}");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Subtraction
            Console.WriteLine("- (Subtraction): deducts the value of numbers");
            Console.WriteLine();
            Console.WriteLine($"Example: {a} - {b} = {a - b}");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Division
            Console.WriteLine("/ (Division): divides the value of numbers");
            Console.WriteLine();
            Console.WriteLine($"Example: {e} / {f} = {e / f}");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Multiplication
            Console.WriteLine("* (Multiplication): multiplies the value of numbers");
            Console.WriteLine();
            Console.WriteLine($"Example: {a} * {b} = {a * b}");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Increment
            Console.WriteLine("++ (Increment): adds the value of the number by 1");
            Console.WriteLine();
            Console.WriteLine($"Example: initial value = {c}, after ++ = {c++}, then final value = {c}");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Decrement
            Console.WriteLine("-- (Decrement): deducts the value of the number by 1");
            Console.WriteLine();
            Console.WriteLine($"Example: initial value = {d}, after -- = {d--}, then final value = {d}");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();

            // Modulus
            Console.WriteLine("% (Modulus): gives the remainder of the value of numbers");
            Console.WriteLine();
            Console.WriteLine($"Example: {a} % {b} = {a % b}");
            Console.WriteLine();
            
            Console.WriteLine("∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘-∘");
            Console.WriteLine();

            // Goodbye message
            Console.WriteLine("Goodbye! Thanks for playing :D");
        }
    }
}

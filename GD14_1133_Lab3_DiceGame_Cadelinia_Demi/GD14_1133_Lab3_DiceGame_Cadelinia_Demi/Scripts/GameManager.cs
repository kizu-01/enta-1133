using Microsoft.VisualBasic;
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
        private AIController ai = new AIController();

        public void Play()
        {
            // Run full intro only once
            if (player == null || computer == null)
                FullIntro();

            // Show start game prompt
            StartGamePrompt();

            // Reset scores and dice pools
            player.ResetPlayer();
            computer.ResetPlayer();

            Instructions();

            for (int round = 1; round <= 3; round++)
            {
                Console.WriteLine("-----------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine($"Round {round}");
                Console.WriteLine();

                // Decide who rolls first
                bool playerFirst = rd.Next(2) == 0;

                int playerDie = 0;
                int compDie = 0;
                int playerRoll = 0;
                int compRoll = 0;

                if (playerFirst)
                {
                    Helper.Typewrite($"{player.Name} will roll first this round!");
                    Console.WriteLine();

                    // Player chooses die first
                    playerDie = PlayerChooseDie();
                    compDie = ai.ChooseDie(computer.DicePool);

                    // Remove selected dice from pools
                    player.DicePool.Remove(playerDie);
                    computer.DicePool.Remove(compDie);

                    // Roll
                    playerRoll = new Turn(player, dice).DoTurn(playerDie);
                    compRoll = new Turn(computer, dice).DoTurn(compDie);
                }
                else
                {
                    Helper.Typewrite($"{computer.Name} will roll first this round!");
                    Console.WriteLine();

                    // Computer chooses and rolls first
                    compDie = ai.ChooseDie(computer.DicePool);
                    compRoll = new Turn(computer, dice).DoTurn(compDie);
                    computer.DicePool.Remove(compDie);

                    // THEN player chooses die and rolls
                    playerDie = PlayerChooseDie();
                    player.DicePool.Remove(playerDie);
                    playerRoll = new Turn(player, dice).DoTurn(playerDie);
                }

                // Handle tie rolls and determine winner
                string condition = ai.ChooseCondition();
                Helper.Typewrite($"The odds have decided: {condition.ToUpper()} roll wins!");

                while (playerRoll == compRoll)
                {
                    Helper.Typewrite("It's a tie! Roll again...");
                    playerRoll = new Turn(player, dice).DoTurn(playerDie);
                    compRoll = new Turn(computer, dice).DoTurn(compDie);
                }

                bool playerWins = (condition == "higher" && playerRoll > compRoll) ||
                                  (condition == "lower" && playerRoll < compRoll);

                if (playerWins)
                {
                    Helper.Typewrite($"{player.Name} wins this round!");
                    player.AddPoint();
                }
                else
                {
                    Helper.Typewrite($"{computer.Name} wins this round!");
                    computer.AddPoint();
                }

                Console.WriteLine();
                Helper.Pause();
                Console.Clear();
            }

            // Show final results
            Outro();
        }
        // Show welcome message
        private void FullIntro()
        {
            string asciiSky = @"
.         _  .          .          .    +     .          .          .      .
        .(_)          .            .            .            .       :
        .   .      .    .     .     .    .      .   .      . .  .  -+-        .
          .           .   .        .           .          /         :  .
    . .        .  .      /.   .      .    .     .     .  / .      . ' .
        .  +       .    /     .          .          .   /      .
       .            .  /         .            .        *   .         .     .
      .   .      .    *     .     .    .      .   .       .  .
          .           .           .           .           .         +  .
  . .        .  .       .   .      .    .     .     .    .      .   .

 .   +      .          ___/\_._/~~\_...__/\__.._._/~\        .         .   .
       .          _.--'                              `--./\          .   .
           /~~\/~\                                         `-/~\_            .
 .      .-'                                                      `-/\_
  _/\.-'                                                          __/~\/\-.__
.'                                                                           `.
";

            Console.WriteLine(asciiSky);
            Console.WriteLine();

            Helper.Typewrite("In the digital world of Demi's ENTAverse, the three lucky dice lived in harmony. ");
            Helper.Typewrite("Then, everything changed when the celestial hosts called The Chance attacked.");
            Helper.Typewrite("Only you, a mere mortal, could stop them. With the help of the lucky dice, ");
            Helper.Typewrite("YOU can save the world. And The Chance shouts...");
            Console.WriteLine();
            Helper.Pause();
            Console.Clear();

            string asciiDice = @"
                                             .-------.    ______
                                            /   o   /|   /\     \
                                           /_______/o|  /o \  o  \
                                           | o     | | /   o\_____\
                                           |   o   |o/ \o   /o    /
                                           |     o |/   \ o/  o  / 
                                           '-------'     \/____o/
";

            Console.WriteLine(asciiDice);

            Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("                                                 Welcome to                                                      ");
            Console.WriteLine();
            Console.WriteLine("██████  ██ ██████      ███████  ██████  ██████       █████       ██████ ██   ██  █████  ███    ██  ██████ ███████ ");
            Console.WriteLine("██   ██ ██      ██     ██      ██    ██ ██   ██     ██   ██     ██      ██   ██ ██   ██ ████   ██ ██      ██   ");
            Console.WriteLine("██   ██ ██  █████      █████   ██    ██ ██████      ███████     ██      ███████ ███████ ██ ██  ██ ██      █████   ");
            Console.WriteLine("██   ██ ██      ██     ██      ██    ██ ██   ██     ██   ██     ██      ██   ██ ██   ██ ██  ██ ██ ██      ██      ");
            Console.WriteLine("██████  ██ ██████      ██       ██████  ██   ██     ██   ██      ██████ ██   ██ ██   ██ ██   ████  ██████ ███████");
            Console.WriteLine();
            Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine();

            Helper.Typewrite("The Chance wants to know your name: ");
            Console.Write(">> ");
            string name = Console.ReadLine();
            player = new Player(name);
            computer = new Player("Opponent");

            Console.WriteLine();
        }

        // Start game prompt
        private void StartGamePrompt()
        {
            Helper.Typewrite("Why not play DI3 FOR A CHANCE?");
            Console.WriteLine();

            if (!InputHandler.GetYesNo("Are you ready to play? [Yes/No]"))
            {
                Console.WriteLine();
                Console.WriteLine("The Chance thanks you for playing DI3 FOR A CHANCE. Until then, mortal. Farewell!");
                Environment.Exit(0);
            }

            Console.WriteLine();
            Console.WriteLine("Great, Let's Play!");
        }

        // Show game instructions
        private void Instructions()
        {
            Console.WriteLine();
            Helper.Typewrite("How To Play:");
            Helper.Typewrite("1. There are 3 rounds in total.");
            Helper.Typewrite("2. Each player has 3 lucky dice: d7, d12, & d21");
            Helper.Typewrite("3. The Chance uses the odds to decide the turn order and winning condition each round.");
            Helper.Typewrite("4. Once a chosen die is used, it'll be removed from the pool.");
            Helper.Typewrite("5. If rolls resulted in a tie, the players get to reroll the current selected die until unique values are rolled.");
            Helper.Typewrite("6. The one with the most points wins the game!");
            Console.WriteLine();
            Helper.Typewrite("May the odds be in your favor. Let the game begin!");
            Console.WriteLine();
            Helper.Pause();
        }
        // Player roll turn
        private int PlayerChooseDie()
        {
            string choice;
            bool valid = false;
            int selectedDie = 0;

            do
            {
                Helper.Typewrite($"{player.Name}, choose your die:");
                foreach (int d in player.DicePool) Console.WriteLine($"- d{d}");
                Console.Write(">> ");
                choice = Console.ReadLine().ToLower();
                Console.WriteLine();

                // Confirm choice
                if (choice.StartsWith("d") && int.TryParse(choice.Substring(1), out int sides) && player.DicePool.Contains(sides))
                {
                    selectedDie = sides;
                    valid = true;
                }
                else
                {
                    Console.Clear(); // clears screen to make it cleaner
                    Console.WriteLine("Invalid choice, please try again.\n");
                }

            } while (!valid);

            return selectedDie;
        }

        private void Outro()
        {
            // Score result
            Console.WriteLine();
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Helper.Typewrite($"Final Score: {player.Name} {player.Score} - {computer.Score} {computer.Name}");
            Console.WriteLine();

            if (player.Score > computer.Score)
                Helper.Typewrite($"{player.Name} WINS!");
            else if (computer.Score > player.Score)
                Helper.Typewrite($"{computer.Name} WINS!");
            else
                Helper.Typewrite("It’s a DRAW!");

            Console.WriteLine();
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine();
        }
    }
}
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
        private Player computer; // used for standalone battles or opponent templates
        private Die dice = new Die();
        private Random rd = new Random();
        private AIController ai = new AIController();
        private bool playerDefeated = false;

        // Track encounter (combat) wins
        private int encountersWon = 0;

        // Other classes can access the player instance (EncounterRoom)
        public Player PlayerInstance => player;

        public void ShowIntroAndInstructions()
        {
            FullIntro();
        }

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
            string name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name)) name = "Player";
            player = new Player(name);
            computer = new Player("Opponent");
            Console.WriteLine();
        }

        private bool StartGamePrompt()
        {
            Helper.Typewrite("Why not play DI3 FOR A CHANCE?");
            Console.WriteLine();
            return InputHandler.GetYesNo("Are you ready to play? [Yes/No]");
        }

        /// <summary>
        /// Instructions
        /// </summary>
        private void Instructions()
        {
            Console.WriteLine();
            Console.WriteLine("How To Play:");
            Console.WriteLine("1. Explore the mysterious dungeon.");
            Console.WriteLine("2. Search Treasure Rooms to find dice (d7, d12, d21).");
            Console.WriteLine("3. Entering an Encounter room lets you decide to fight (you must have at least a die at hand).");
            Console.WriteLine("4. Battles have up to 3 rounds depending on how many dice you own (1 = 1 round, 2 = 2 rounds, 3 = 3 rounds).");
            Console.WriteLine("5. When you use a die, it will be removed from your pool.");
            Console.WriteLine("6. During battle, The Chance uses the odds to decide higher/lower value and turn order each round.");
            Console.WriteLine("7. If you win the match, you get to keep your dice you currently possess. If you lose, it's game over.");
            Console.WriteLine("5. Win the game by defeating all 3 opponents lingering the dungeon!");
            Console.WriteLine();
            Helper.Typewrite("May the odds be in your favor. Let the game begin!");
            Console.WriteLine();
            Helper.Pause();
        }

        public void StartDungeon()
        {
            while (true)
            {
                Console.Clear();
                if (!StartGamePrompt())
                {
                    Helper.Typewrite("Until then, mortal. Farewell!");
                    Environment.Exit(0);
                }

                Console.WriteLine();
                Console.WriteLine("Great, Let's Play!");
                Instructions();

                Dungeon dungeon = new Dungeon(3, 3, player);
                GenerateRandomDungeon(dungeon);

                bool inSession = true;
                while (inSession)
                {
                    Console.Clear();
                    dungeon.DisplayMap();
                    Room current = dungeon.CurrentRoom();
                    current.OnEnteredRoom();

                    bool chosen = false;
                    while (!chosen)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Choose your action:");
                        Console.WriteLine("1) Search");
                        Console.WriteLine("2) Move");
                        Console.WriteLine("3) Inventory");
                        Console.WriteLine("4) Exit");
                        Console.Write(">> ");
                        string action = Console.ReadLine().Trim();
                        Console.WriteLine();

                        switch (action)
                        {
                            case "1":
                                current.OnSearchedRoom(player);
                                chosen = true;
                                break;

                            case "2":
                                string dir = AskDirection();
                                bool moved = dungeon.Move(dir);
                                Console.WriteLine();
                                if (moved)
                                {
                                    Console.WriteLine($"You moved {dir.ToUpper()} to {dungeon.CurrentRoom().Name}.");
                                    chosen = true;
                                }
                                else
                                {
                                    Console.WriteLine("Can't go that way. Press Enter to choose again.");
                                    Console.ReadLine();
                                    Console.Clear();
                                    dungeon.DisplayMap();
                                    current.OnEnteredRoom();
                                }
                                break;

                            case "3":
                                ShowInventory();
                                break;

                            case "4":
                                Helper.Typewrite("The Chance thanks you for playing DI3 FOR A CHANCE. Until then, mortal. Farewell!");
                                return;

                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid input. Please choose 1-4.");
                                dungeon.DisplayMap();
                                current.OnEnteredRoom();
                                break;
                        }
                    }

                    if (playerDefeated)
                    {
                        DisplayGameOver(ref inSession);
                    }
                }
            }
        }

        private void DisplayGameOver(ref bool inSession)
        {
            Console.Clear();
            Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("   █████████    █████████   ██████   ██████ ██████████       ███████    █████   █████ ██████████ ███████████  ");
            Console.WriteLine("  ███▒▒▒▒▒███  ███▒▒▒▒▒███ ▒▒██████ ██████ ▒▒███▒▒▒▒▒█     ███▒▒▒▒▒███ ▒▒███   ▒▒███ ▒▒███▒▒▒▒▒█▒▒███▒▒▒▒▒███ ");
            Console.WriteLine(" ███     ▒▒▒  ▒███    ▒███  ▒███▒█████▒███  ▒███  █ ▒     ███     ▒▒███ ▒███    ▒███  ▒███  █ ▒  ▒███    ▒███ ");
            Console.WriteLine("▒███          ▒███████████  ▒███▒▒███ ▒███  ▒██████      ▒███      ▒███ ▒███    ▒███  ▒██████    ▒██████████  ");
            Console.WriteLine("▒███    █████ ▒███▒▒▒▒▒███  ▒███ ▒▒▒  ▒███  ▒███▒▒█      ▒███      ▒███ ▒▒███   ███   ▒███▒▒█    ▒███▒▒▒▒▒███ ");
            Console.WriteLine("▒▒███  ▒▒███  ▒███    ▒███  ▒███      ▒███  ▒███ ▒   █   ▒▒███     ███   ▒▒▒█████▒    ▒███ ▒   █ ▒███    ▒███ ");
            Console.WriteLine(" ▒▒█████████  █████   █████ █████     █████ ██████████    ▒▒▒███████▒      ▒▒███      ██████████ █████   █████");
            Console.WriteLine("  ▒▒▒▒▒▒▒▒▒  ▒▒▒▒▒   ▒▒▒▒▒ ▒▒▒▒▒     ▒▒▒▒▒ ▒▒▒▒▒▒▒▒▒▒       ▒▒▒▒▒▒▒         ▒▒▒      ▒▒▒▒▒▒▒▒▒▒ ▒▒▒▒▒   ▒▒▒▒▒ ");
            Console.WriteLine();
            Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Helper.Typewrite("                  The chosen mortal has fallen, and the ENTAverse lives on in jeopardy.");
            Console.WriteLine();
            Console.WriteLine("═════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine();

            if (InputHandler.GetYesNo("Do you want to play again? [Yes/No]"))
            {
                player.ResetPlayer(true);
                encountersWon = 0;
                playerDefeated = false;
                inSession = false;
            }
            else
            {
                Console.WriteLine();
                Helper.Typewrite("Until then, mortal. Farewell!");
                Environment.Exit(0);
            }
        }

        private void GenerateRandomDungeon(Dungeon dungeon)
        {
            var treasureNames = new[] { "Golden Cavern", "Crystal Trench", "Heaven's Vault" };
            var treasureDice = new[] { 7, 12, 21 };
            var encounterNames = new[] { "Lair of Misfortune", "Wretched Abyss", "Pandora's Chamber" };

            List<Room> roomsToPlace = new List<Room>();
            for (int i = 0; i < 3; i++)
                roomsToPlace.Add(new TreasureRoom(treasureNames[i], treasureDice[i]));
            for (int i = 0; i < 3; i++)
                roomsToPlace.Add(new EncounterRoom(encounterNames[i], this));
            roomsToPlace.Add(new HealRoom("Sanctum of Renewal"));
            roomsToPlace.Add(new TrapRoom("Pit of Deceit"));

            Shuffle(roomsToPlace);
            dungeon.SetRoom(1, 1, new EmptyRoom("Hollow Room"));

            int idx = 0;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (x == 1 && y == 1) continue;
                    dungeon.SetRoom(x, y, roomsToPlace[idx++]);
                }
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rd.Next(i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public bool Battle(Player opponent)
        {
            // Reset opponent each battle
            opponent.ResetPlayer(true);
            opponent.AddDie(7);
            opponent.AddDie(12);
            opponent.AddDie(21);

            if (!player.HasDice())
            {
                Helper.Typewrite("You have no dice in possession. You cannot enter battle!");
                Helper.Pause();
                playerDefeated = true;
                return false;
            }

            int rounds = Math.Min(3, player.DicePoolCount);
            player.ResetScore();
            opponent.ResetScore();

            // Backup dice for tiebreakers
            List<int> playerDiceBackup = new List<int>(player.DicePoolCopy);

            for (int round = 1; round <= rounds; round++)
            {
                Console.Clear();
                Helper.Typewrite($"-----------------------------------------------------------------------------");
                Console.WriteLine();
                Helper.Typewrite($"Round {round}");
                Console.WriteLine();

                bool playerFirst = rd.Next(2) == 0;
                int playerDie = 0, oppDie = 0;
                int playerRoll = 0, oppRoll = 0;

                if (playerFirst)
                {
                    Helper.Typewrite($"{player.Name} rolls first!");
                    Console.WriteLine();
                    playerDie = PlayerChooseDie();
                    player.RemoveDie(playerDie);
                    playerRoll = new Turn(player, dice).DoTurn(playerDie);

                    oppDie = ai.ChooseDie(opponent.DicePoolCopy);
                    opponent.RemoveDie(oppDie);
                    oppRoll = new Turn(opponent, dice).DoTurn(oppDie);
                }
                else
                {
                    Helper.Typewrite($"{opponent.Name} rolls first!");
                    Console.WriteLine();
                    oppDie = ai.ChooseDie(opponent.DicePoolCopy);
                    opponent.RemoveDie(oppDie);
                    oppRoll = new Turn(opponent, dice).DoTurn(oppDie);

                    playerDie = PlayerChooseDie();
                    player.RemoveDie(playerDie);
                    playerRoll = new Turn(player, dice).DoTurn(playerDie);
                }

                string condition = ai.ChooseCondition();
                Helper.Typewrite($"The odds have decided: {condition.ToUpper()} roll wins!");

                // Handle tie score situations
                while (playerRoll == oppRoll)
                {
                    Helper.Typewrite("It's a tie! Both sides will reroll their dice...");
                    playerRoll = new Turn(player, dice).DoTurn(playerDie);
                    oppRoll = new Turn(opponent, dice).DoTurn(oppDie);
                    Helper.Typewrite($"Reroll Results = {player.Name}: {playerRoll} | {opponent.Name}: {oppRoll}");
                }

                bool playerWins = (condition == "higher" && playerRoll > oppRoll) ||
                                  (condition == "lower" && playerRoll < oppRoll);

                if (playerWins)
                {
                    Helper.Typewrite($"{player.Name} wins this round!");
                    player.AddPoint();
                }
                else
                {
                    Helper.Typewrite($"{opponent.Name} wins this round!");
                    opponent.AddPoint();
                }

                Helper.Pause();
            }

            Console.Clear();
            Helper.Typewrite($"-----------------------------------------------------------------------------");
            Console.WriteLine();
            Helper.Typewrite($"Final Score: {player.Name} {player.Score} - {opponent.Score} {opponent.Name}");
            Console.WriteLine();
            Console.WriteLine($"| | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | |");
            Console.WriteLine();

            if (player.Score > opponent.Score)
            {
                Helper.Typewrite($"{player.Name} wins the battle!");
                Console.WriteLine();
                Console.WriteLine($"-----------------------------------------------------------------------------");
                encountersWon++;

                // Restore dice used during battle
                player.RestoreDicePool(playerDiceBackup);

                // Check for game completion
                if (encountersWon >= 3)
                {
                    Console.WriteLine();
                    Helper.Typewrite("═══════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine();
                    Helper.Typewrite("Congratulations! You have defeated all opponents and conquered DI3!");
                    Helper.Typewrite("The world of the ENTAverse is safe once more!");
                    Console.WriteLine();
                    Helper.Typewrite("═══════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine();

                    if (InputHandler.GetYesNo("Do you want to play again? [Yes/No]"))
                    {
                        player.ResetPlayer(true);
                        encountersWon = 0;
                        playerDefeated = false;
                        Console.Clear();
                        Helper.Typewrite("Why not play DI3 FOR A CHANCE?");
                    }
                    else
                    {
                        Helper.Typewrite("Farewell, mortal. Until next time...");
                        Environment.Exit(0);
                    }
                }

                return true;
            }
            else
            {
                Helper.Typewrite("Opponent wins the battle!");
                playerDefeated = true;
                return false;
            }
        }

        private int PlayerChooseDie()
        {
            string choice;
            bool valid = false;
            int selectedDie = 0;

            do
            {
                Helper.Typewrite($"{player.Name}, choose your die:");
                foreach (int d in player.DicePoolCopy) Console.WriteLine($"- d{d}");
                Console.Write(">> ");
                choice = Console.ReadLine().ToLower().Trim();

                if (choice.StartsWith("d") && int.TryParse(choice.Substring(1), out int sides) && player.DicePoolCopy.Contains(sides))
                {
                    selectedDie = sides;
                    valid = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice, please try again.");
                }
            } while (!valid);

            return selectedDie;
        }

        private string AskDirection()
        {
            Console.WriteLine();
            Helper.Typewrite("Choose a direction:");
            Console.WriteLine("1) North");
            Console.WriteLine("2) South");
            Console.WriteLine("3) East");
            Console.WriteLine("4) West");
            Console.Write(">> ");
            string choice = Console.ReadLine().Trim();
            switch (choice)
            {
                case "1": return "north";
                case "2": return "south";
                case "3": return "east";
                case "4": return "west";
                default:
                    Console.WriteLine("Invalid input. Defaulting to stay in place.");
                    return "none";
            }
        }

        private void ShowInventory()
        {
            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════");
            Console.WriteLine("            INVENTORY");
            Console.WriteLine("═══════════════════════════════════");

            if (player.DicePoolCount == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Your inventory is empty.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                foreach (int die in player.DicePoolCopy)
                    Console.WriteLine($"- d{die}");
            }

            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("- Press Enter to return -");
            Console.ReadLine();
        }
    }
}
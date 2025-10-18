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
        private Die dice = new Die();
        private Random rd = new Random();
        private AIController ai = new AIController();
        private bool playerDefeated = false;
        private Dungeon dungeon; // class-level instance

        // Track encounter (combat) wins
        private int encountersWon = 0;

        // Other classes can access the player instance (EncounterRoom)
        public Player PlayerInstance => player;

        public void ShowIntroAndInstructions()
        {
            // Move ascii/UI prints to UI class
            UI.ShowSkyArt();
            Helper.Typewrite("In the digital world of Demi's ENTAverse, the three lucky dice lived in harmony. ");
            Helper.Typewrite("Then, everything changed when the celestial hosts called The Chance took over.");
            Helper.Typewrite("Only you, a mere mortal, could stop the cursed beasts lingering the dungeon.");
            Helper.Typewrite("With the help of the lucky dice transformed into powerful weapons...");
            Helper.Typewrite("YOU can save the world. And a voice chants to you-");
            Console.WriteLine();
            Helper.Pause();
            Console.Clear();

            UI.ShowDiceArt();
            UI.ShowTitleBanner();

            Helper.Typewrite("The Chance wants to know your name: ");
            Console.Write(">> ");
            string name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name)) name = "Mortal";
            player = new Player(name);
            Console.WriteLine();
        }

        private bool StartGamePrompt()
        {
            Helper.Typewrite($"Okay, {player.Name}.");
            Console.WriteLine();
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
            Console.WriteLine("2. Search Treasure Rooms to find dice-weapons (d7, d12, d21).");
            Console.WriteLine("3. Entering an Encounter room lets you decide to fight (you must have at least a weapon).");
            Console.WriteLine("4. Battles are HP-based. Each weapon roll deals damage equal to its dice roll.");
            Console.WriteLine("5. Weapons are reusable. Potions are consumable.");
            Console.WriteLine("6. Wrong input on chosen weapon during battle will default you to use the first weapon obtained.");
            Console.WriteLine("7. Win the game by defeating all 3 opponents lingering the dungeon!");
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

                // Assign to class-level dungeon to give access for other methods (Battle)
                dungeon = new Dungeon(3, 3, player);
                GenerateRandomDungeon(dungeon);

                bool inSession = true;
                while (inSession)
                {
                    Console.Clear();
                    dungeon.DisplayMap();
                    Room current = dungeon.CurrentRoom();
                    current.OnEnteredRoom();

                    Console.WriteLine();
                    UI.ShowHP(player.Name, player.HP, player.MaxHP);

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
                                chosen = false; // remain in the same room after inventory
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
            UI.ShowGameOver();

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
            // Provide dice values for weapons and names for encounter and treasure rooms
            var treasureNames = new[] { "Golden Cavern", "Crystal Trench", "Heaven's Vault" };
            var treasureDice = new[] { 7, 12, 21 }; // used to decide weapon dice
            var encounterNames = new[] { "Lair of Misfortune", "Wretched Abyss", "Pandora's Chamber" };

            List<Room> roomsToPlace = new List<Room>();

            // Treasure rooms rewards weapons while keeping dice sides mapping
            for (int i = 0; i < 3; i++)
            {
                // 7 -> dagger (d7), 12 -> longsword (d12), 21 -> sledgehammer (d21)
                string weaponName = treasureDice[i] switch
                {
                    7 => "Dagger (d7)",
                    12 => "Longsword (d12)",
                    21 => "Sledgehammer (d21)",
                    _ => $"Weapon (d{treasureDice[i]})"
                };
                roomsToPlace.Add(new TreasureRoom(treasureNames[i], treasureDice[i], weaponName));
            }

            // Encounter rooms
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

        // HP-based battle using weapons (both player and opponent have HP)
        public bool Battle(Player opponentTemplate)
        {
            // Create opponent instance with reusable default weapons
            Player opponent = new Player("The Cursed Beast", 50);
            opponent.ResetPlayer(true);
            // Give opponent similar weapons
            opponent.AddItem(new Weapon("Enemy Dagger (d7)", 1, 7));
            opponent.AddItem(new Weapon("Enemy Longsword (d12)", 1, 12));
            opponent.AddItem(new Weapon("Enemy Sledgehammer (d21)", 1, 21));
            opponent.ResetHP();

            // To certify player must have at least one weapon to fight
            if (!player.HasWeapons())
            {
                Helper.Typewrite("You have no weapons. You cannot enter battle!");
                Helper.Pause();
                playerDefeated = true;
                return false;
            }

            // To make sure player has HP
            if (player.HP <= 0)
            {
                Helper.Typewrite("You have no HP left. You cannot fight!");
                playerDefeated = true;
                return false;
            }

            UI.ShowMessage($"Battle begins: {player.Name} vs {opponent.Name}!");
            Helper.Pause();

            // Random turn order display
            bool playerStarts = rd.Next(2) == 0;
            Helper.Typewrite(playerStarts ? $"{player.Name} will roll first this exchange." : $"{opponent.Name} will roll first this exchange.");
            Helper.Pause();

            while (!player.IsDead && !opponent.IsDead)
            {
                Console.Clear();
                UI.ShowCombatStatus(player, opponent);

                // Allow potions to be used before attack
                if (player.GetConsumables().Count > 0)
                {
                    Helper.Typewrite("Do you want to use a consumable before attacking?");
                    if (InputHandler.GetYesNo("[Yes/No]"))
                    {
                        var consumables = player.GetConsumables();
                        for (int i = 0; i < consumables.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) {consumables[i].DisplayName}");
                        }
                        Console.Write(">> ");
                        int sel;
                        if (!int.TryParse(Console.ReadLine().Trim(), out sel) || sel < 1 || sel > consumables.Count)
                            sel = 1;
                        int healed = player.UseConsumable(consumables[sel - 1], dice);
                        Console.WriteLine();
                        Helper.Typewrite($"You used {consumables[sel - 1].DisplayName} and restored {healed} HP!");
                        Console.WriteLine();
                        Helper.Pause();
                    }
                }

                Weapon playerWeapon = null;
                Weapon opponentWeapon = null;

                var playerWeapons = player.GetWeapons();
                if (playerWeapons.Count == 0)
                {
                    Helper.Typewrite("You have no weapon to attack with.");
                    Helper.Pause();
                    playerDefeated = true;
                    return false;
                }

                Console.WriteLine();
                Helper.Typewrite("Choose your weapon:");
                for (int i = 0; i < playerWeapons.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {playerWeapons[i].DisplayName} ({playerWeapons[i].DiceCount}d{playerWeapons[i].DiceSides})");
                }
                Console.Write(">> ");
                int chosenIndex = 1;
                if (!int.TryParse(Console.ReadLine().Trim(), out chosenIndex) || chosenIndex < 1 || chosenIndex > playerWeapons.Count)
                {
                    Console.WriteLine();
                    Helper.Typewrite("Invalid selection, defaulting to first weapon.");
                    chosenIndex = 1;
                }
                playerWeapon = playerWeapons[chosenIndex - 1];

                opponentWeapon = ai.ChooseWeapon(opponent.GetWeapons());

                var turnPlayer = new Turn(player, dice);
                var turnOpp = new Turn(opponent, dice);

                // Turn order: show who goes first (playerStarts random per battle start)
                Console.WriteLine();
                int playerRoll = 0, oppRoll = 0;
                if (playerStarts)
                {
                    playerRoll = turnPlayer.DoUseRaw(playerWeapon);
                    Helper.Typewrite($"{player.Name} rolled: {playerRoll}");
                    Console.WriteLine();
                    oppRoll = turnOpp.DoUseRaw(opponentWeapon);
                    Helper.Typewrite($"{opponent.Name} rolled: {oppRoll}");
                    Console.WriteLine();
                }
                else
                {
                    oppRoll = turnOpp.DoUseRaw(opponentWeapon);
                    Helper.Typewrite($"{opponent.Name} rolled: {oppRoll}");
                    Console.WriteLine();
                    playerRoll = turnPlayer.DoUseRaw(playerWeapon);
                    Helper.Typewrite($"{player.Name} rolled: {playerRoll}");
                    Console.WriteLine();
                }

                // The odds: choose higher or lower for this exchange (after rolls)
                bool higherWins = rd.Next(2) == 0;
                Helper.Typewrite($"The odds have decided: {(higherWins ? "HIGHER" : "LOWER")} roll wins this exchange!");
                Console.WriteLine();

                // In case of tie, reroll until break (matching roll order)
                while (playerRoll == oppRoll)
                {
                    Helper.Typewrite("It's a tie! Both sides reroll their chosen weapons...");
                    Console.WriteLine();
                    if (playerStarts)
                    {
                        playerRoll = turnPlayer.DoUseRaw(playerWeapon);
                        Helper.Typewrite($"{player.Name} rerolled: {playerRoll}");
                        Console.WriteLine();
                        oppRoll = turnOpp.DoUseRaw(opponentWeapon);
                        Helper.Typewrite($"{opponent.Name} rerolled: {oppRoll}");
                        Console.WriteLine();
                    }
                    else
                    {
                        oppRoll = turnOpp.DoUseRaw(opponentWeapon);
                        Helper.Typewrite($"{opponent.Name} rerolled: {oppRoll}");
                        Console.WriteLine();
                        playerRoll = turnPlayer.DoUseRaw(playerWeapon);
                        Helper.Typewrite($"{player.Name} rerolled: {playerRoll}");
                        Console.WriteLine();
                    }
                }

                bool playerWins = higherWins ? playerRoll > oppRoll : playerRoll < oppRoll;

                if (playerWins)
                {
                    Helper.Typewrite($"{player.Name} wins the exchange and deals {playerRoll} damage!");
                    Console.WriteLine();
                    opponent.TakeDamage(playerRoll);
                }
                else
                {
                    Helper.Typewrite($"{opponent.Name} wins the exchange and deals {oppRoll} damage!");
                    Console.WriteLine();
                    player.TakeDamage(oppRoll);
                }

                Helper.Pause();
            }

            Console.Clear();
            UI.ShowCombatStatus(player, opponent);

            if (!player.IsDead && opponent.IsDead)
            {
                Helper.Typewrite($"{player.Name} wins the battle!");
                encountersWon++;

                // 50% chance drop big potion
                if (rd.Next(100) < 50)
                {
                    var drop = new Consumable("Big Healing Potion (3d12)", 3, 12);
                    player.AddItem(drop);
                    Console.WriteLine();
                    Helper.Typewrite($"You found a {drop.DisplayName} after the fight!");
                    Console.WriteLine();
                }

                // Reset HealRooms to allow new potions after battle
                if (dungeon != null)
                {
                    foreach (var room in dungeon.GetAllRooms().OfType<HealRoom>())
                    {
                        room.ResetTaken();
                    }
                }

                Helper.Pause();

                if (encountersWon >= 3)
                {
                    Console.Clear();
                    UI.ShowCongratulations();
                    Helper.Pause();

                    Console.Clear();
                    UI.ShowClosingThanks();
                    Helper.Pause();

                    if (InputHandler.GetYesNo("Do you want to play again? [Yes/No]"))
                    {
                        // Reset everything properly
                        player.ResetPlayer(true);
                        encountersWon = 0;
                        playerDefeated = false;

                        Console.Clear();
                        Helper.Typewrite("Why not play DI3 FOR A CHANCE?");

                        return true;
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
                Helper.Typewrite($"{player.Name} was defeated. You lost the battle.");
                playerDefeated = true;
                Helper.Pause();
                return false;
            }
        }

        // Show inventory and can use consumables and equip weapons in exploration
        private void ShowInventory()
        {
            bool done = false;
            while (!done)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine("            INVENTORY");
                Console.WriteLine("═══════════════════════════════════");
                Console.WriteLine();
                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("Your inventory is empty.");
                }
                else
                {
                    int i = 1;
                    foreach (var it in player.Inventory)
                    {
                        Console.WriteLine($"{i}) {it.DisplayName}");
                        i++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Actions:");
                Console.WriteLine("1) Use Consumable");
                Console.WriteLine("2) Back");
                Console.Write(">> ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        var pots = player.GetConsumables();
                        if (pots.Count == 0)
                        {
                            Helper.Typewrite("You have no consumables.");
                            Helper.Pause();
                        }
                        else
                        {
                            Helper.Typewrite("Choose a consumable to use:");
                            for (int i = 0; i < pots.Count; i++)
                                Console.WriteLine($"{i + 1}) {pots[i].DisplayName}");
                            Console.Write(">> ");
                            if (int.TryParse(Console.ReadLine().Trim(), out int sel) && sel >= 1 && sel <= pots.Count)
                            {
                                int healed = player.UseConsumable(pots[sel - 1], dice);

                                Console.WriteLine();

                                if (healed == -1)
                                {
                                    // HP full: don’t show “used” message
                                    Helper.Typewrite("Your HP is full! The potion remains in your inventory.");
                                }
                                else if (healed > 0)
                                {
                                    // Normal heal
                                    Helper.Typewrite($"You used {pots[sel - 1].DisplayName} and restored {healed} HP!");
                                    Helper.Typewrite($"Current HP: {player.HP}/{player.MaxHP}");
                                }
                                else
                                {
                                    // In case something weird happens
                                    Helper.Typewrite("No healing occurred.");
                                }

                                Helper.Pause();
                            }
                            else
                            {
                                Helper.Typewrite("Invalid selection.");
                                Helper.Pause();
                            }
                        }
                        break;

                    case "2":
                        done = true;
                        break;

                    default:
                        Helper.Typewrite("Invalid choice.");
                        Helper.Pause();
                        break;
                }
            }
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
    }
}
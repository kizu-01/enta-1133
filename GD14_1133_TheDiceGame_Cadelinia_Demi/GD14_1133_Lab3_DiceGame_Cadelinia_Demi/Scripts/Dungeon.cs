using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Dungeon
    {
        public Room[,] Grid;
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public int PlayerX { get; private set; }
        public int PlayerY { get; private set; }

        public Player Player { get; private set; }

        public Dungeon(int rows, int cols, Player player)
        {
            if (rows <= 0 || cols <= 0) throw new ArgumentOutOfRangeException();
            Rows = rows;
            Cols = cols;
            Grid = new Room[Rows, Cols];
            Player = player;
            PlayerX = 1;
            PlayerY = 1;
        }

        public void SetRoom(int x, int y, Room room)
        {
            if (x < 0 || x >= Rows || y < 0 || y >= Cols) throw new ArgumentOutOfRangeException();
            Grid[x, y] = room;
            LinkNeighborsForAll(); // relink neighbors
        }

        private void LinkNeighborsForAll()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                {
                    var room = Grid[i, j];
                    if (room == null) continue;
                    room.North = (i > 0) ? Grid[i - 1, j] : null;
                    room.South = (i < Rows - 1) ? Grid[i + 1, j] : null;
                    room.West = (j > 0) ? Grid[i, j - 1] : null;
                    room.East = (j < Cols - 1) ? Grid[i, j + 1] : null;
                }
        }

        public Room CurrentRoom() => Grid[PlayerX, PlayerY];

        // Direction: "north","south","east","west"
        public bool Move(string direction)
        {
            direction = direction?.ToLower()?.Trim();
            switch (direction)
            {
                case "north":
                    if (PlayerX > 0 && Grid[PlayerX - 1, PlayerY] != null)
                    {
                        PlayerX--;
                        return true;
                    }
                    break;
                case "south":
                    if (PlayerX < Rows - 1 && Grid[PlayerX + 1, PlayerY] != null)
                    {
                        PlayerX++;
                        return true;
                    }
                    break;
                case "east":
                    if (PlayerY < Cols - 1 && Grid[PlayerX, PlayerY + 1] != null)
                    {
                        PlayerY++;
                        return true;
                    }
                    break;
                case "west":
                    if (PlayerY > 0 && Grid[PlayerX, PlayerY - 1] != null)
                    {
                        PlayerY--;
                        return true;
                    }
                    break;
            }
            return false;
        }

        // Display map with player marker
        public void DisplayMap()
        {
            Console.WriteLine();
            Console.WriteLine("| N ⌃ | S v | E › | W ‹ |");
            Console.WriteLine();
            for (int i = 0; i < Rows; i++)
            {
                // top border for each row
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write("+-----");
                }
                Console.WriteLine("+");

                // content line
                for (int j = 0; j < Cols; j++)
                {
                    string content = "     ";
                    if (i == PlayerX && j == PlayerY) content = "  X  ";
                    else if (Grid[i, j] != null) content = "  ■  ";
                    Console.Write($"|{content}");
                }
                Console.WriteLine("|");
            }
            for (int j = 0; j < Cols; j++) Console.Write("+-----");
            Console.WriteLine("+");
            Console.WriteLine();
        }

        // Return all rooms in the dungeon as an enumerable
        public IEnumerable<Room> GetAllRooms()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    if (Grid[i, j] != null)
                        yield return Grid[i, j];
        }
    }
}
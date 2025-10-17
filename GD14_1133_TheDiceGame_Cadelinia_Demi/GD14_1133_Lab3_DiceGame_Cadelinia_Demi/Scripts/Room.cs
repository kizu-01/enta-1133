using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal abstract class Room
    {
        public string Name { get; protected set; }
        public bool Visited { get; protected set; }

        public Room North { get; set; }
        public Room South { get; set; }
        public Room East { get; set; }
        public Room West { get; set; }

        public Room(string name)
        {
            Name = name;
            Visited = false;
        }

        public virtual void OnEnteredRoom()
        {
            Console.WriteLine();
            if (!Visited)
            {
                Helper.Typewrite($"You entered {Name}. {RoomDescription()}");
                Visited = true;
            }
            else
            {
                Helper.Typewrite($"You entered {Name}. You've visited this place before.");
            }
        }

        // return room description text
        public abstract string RoomDescription();

        // called when player chooses "search"
        public abstract void OnSearchedRoom(Player player);

        public virtual void OnExitedRoom()
        {
            Console.WriteLine($"You left {Name}...");
        }

        // helper to reset visited
        public void ResetVisited() => Visited = false;
    }
}
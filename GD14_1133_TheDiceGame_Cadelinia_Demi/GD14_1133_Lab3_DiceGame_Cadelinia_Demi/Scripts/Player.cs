using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class Player
    {
        public string Name { get; private set; }

        // Health
        public int MaxHP { get; private set; } = 50;
        public int HP { get; private set; }

        // Inventory of items (weapons, consumables, etc.)
        public List<Item> Inventory { get; private set; } = new List<Item>();

        // For backwards compatibility or debugging
        public int Score { get; private set; } = 0;

        public Player(string name, int maxHP = 50)
        {
            Name = name;
            MaxHP = maxHP;
            HP = MaxHP;
        }

        // Health methods
        public void ResetHP()
        {
            HP = MaxHP;
        }

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP < 0) HP = 0;
        }

        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP) HP = MaxHP;
        }

        public bool IsDead => HP <= 0;

        // Inventory methods
        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        public bool RemoveItem(Item item)
        {
            return Inventory.Remove(item);
        }

        public List<Weapon> GetWeapons()
        {
            return Inventory.OfType<Weapon>().ToList();
        }

        public List<Consumable> GetConsumables()
        {
            return Inventory.OfType<Consumable>().ToList();
        }

        public bool HasWeapons()
        {
            return GetWeapons().Count > 0;
        }

        // Use consumable which returns heal amount or 0 if nothing used
        public int UseConsumable(Consumable c, Die die)
        {
            if (c == null) return 0;
            if (!Inventory.Contains(c)) return 0;

            // Prevent wasting potion if HP is full
            if (HP >= MaxHP)
            {
                return -1; // signal GameManager that potion wasn't used
            }

            int heal = c.Use(die);
            Heal(heal);
            // remove consumable
            Inventory.Remove(c);
            return heal;
        }

        // Score previously used in round-based system. Keep but not used in HP combat
        public void AddPoint() => Score++;
        public void ResetScore() => Score = 0;

        // Reset player for new game
        public void ResetPlayer(bool resetInventory)
        {
            ResetScore();
            ResetHP();
            if (resetInventory)
            {
                Inventory.Clear();
            }
        }
    }
}
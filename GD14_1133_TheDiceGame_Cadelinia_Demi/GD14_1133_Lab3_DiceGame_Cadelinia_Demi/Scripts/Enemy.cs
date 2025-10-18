using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    // Set HP and attack roll dmg for enemies
    public class Enemy
    {
        public string Name { get; private set; }
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }
        private int AttackDie;

        public Enemy(string name, int hp, int attackDie)
        {
            Name = name;
            MaxHP = hp;
            CurrentHP = hp;
            AttackDie = attackDie;
        }

        public void TakeDamage(int dmg)
        {
            CurrentHP -= dmg;
            if (CurrentHP < 0) CurrentHP = 0;
        }

        public string HPStatus()
        {
            return $"{CurrentHP}/{MaxHP}";
        }

        public int RollAttack()
        {
            Random rand = new Random();
            return rand.Next(1, AttackDie + 1);
        }
    }
}
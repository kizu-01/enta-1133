using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal class AIController
    {
        private Random rd = new Random();

        // Choose a weapon from available weapon list
        public Weapon ChooseWeapon(List<Weapon> availableWeapons)
        {
            Console.WriteLine();
            if (availableWeapons == null || availableWeapons.Count == 0)
                return null;
            int idx = rd.Next(availableWeapons.Count);
            var chosen = availableWeapons[idx];
            Helper.Typewrite($"Opponent chooses {chosen.DisplayName}...");
            return chosen;
        }

        // Decide action (attack or use potion if it exists)
        public string ChooseAction(Player aiPlayer)
        {
            // If AI has consumable and low HP, might choose to heal occasionally
            var pots = aiPlayer.GetConsumables();
            if (aiPlayer.HP < aiPlayer.MaxHP / 3 && pots.Count > 0 && rd.Next(100) < 40)
            {
                return "potion";
            }
            return "attack";
        }
    }
}
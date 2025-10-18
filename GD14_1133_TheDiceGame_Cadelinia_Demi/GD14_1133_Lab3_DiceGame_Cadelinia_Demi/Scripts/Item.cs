using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_Lab3_DiceGame_Cadelinia_Demi.Scripts
{
    internal abstract class Item
    {
        public string DisplayName { get; protected set; }

        // Items have dice-based effects (damage/healing)
        // Returns result of using this item (damage or healing)
        public abstract int Use(Die die); // die passed to perform rolls

        // Some items can be consumed (potions) - false by default
        public virtual bool IsConsumable => false;
    }
}
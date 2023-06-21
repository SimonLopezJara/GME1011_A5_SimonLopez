using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Simple item, increase the player attack
 * Child of Item
 */
namespace GME1011_A5_SimonLopez
{
    internal class AttackPotion : Item
    {
        private int _attack; // just an attribute... what a simple code
        public AttackPotion(Vector2 position, Texture2D sprite, int attack) : base(position,sprite) // constructor
        {
            _attack = attack;
        }

        public int Attack { get { return _attack; } } // return the attack
    }
}

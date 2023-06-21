using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Let's heal our player
 * just a healing item
 */

namespace GME1011_A5_SimonLopez
{
    internal class HealthPotion : Item
    {
        private int _health;
        public HealthPotion(Vector2 position, Texture2D sprite, int health) : base(position, sprite)
        {
            _health = health;
        }

        public int Health { get { return _health; } }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * boring enemie, just a normal zombie
 * 
 */
namespace GME1011_A5_SimonLopez
{
    internal class Zombie : Enemie
    {
        public Zombie(Vector2 position, int health, int strength, Vector2 speed, Texture2D sprite, SpriteFont font) : base(position, health, strength, speed, sprite, font) { }

        public int Bite() //the special attack
        { 
            Debug.WriteLine("Zombie Special Attack"); // just for debug :)
            return _strength*2; 
        }
    }
}

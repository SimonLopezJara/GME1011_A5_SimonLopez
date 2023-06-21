using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * The best enemie of the whole game, vlad, the bat that is seeking for revenge.
 * its faster, its ppowerful and can kill the player super fast.
 * but its a simple class :( the parent is the important class
 */

namespace GME1011_A5_SimonLopez
{
    internal class Vlad : Enemie
    {
        public Vlad (Vector2 position, int health, int strength, Vector2 speed, Texture2D sprite, SpriteFont font) : base (position,health,strength,speed,sprite,font) {}

        public int BloodExtration ()  // the mega super cool attack
        {
            Debug.WriteLine("Vlad Special Attack");
            return _rng.Next(10,40); 
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * My enemie class
 * 
 */
namespace GME1011_A5_SimonLopez
{
    internal class Enemie
    {
        protected Random _rng = new Random(); //all my attributes
        protected int _health, _strength;
        protected Vector2 _positon,_speed;
        protected Texture2D _sprite;
        protected SpriteFont _font;
        public Enemie(Vector2 position,int health, int strength, Vector2 speed, Texture2D sprite, SpriteFont font) {  //my enemie constructor
            _positon = position;
            _health = health;
            _speed = speed;
            _strength = strength;
            _sprite = sprite;
            _font = font;
        }


        public Rectangle Bounds { get { return new Rectangle((int)_positon.X, (int)_positon.Y, 100, 100); } } // get the enemie bounds
        public Vector2 Position { get { return _positon; } } // get the enemie position
        public int Health { get { return _health; } }  // enemie health
        public virtual int Attack() // attack method
        {
            Debug.WriteLine("Enemy Attack");
            if (_rng.NextDouble() < 0.8)
                return _rng.Next(1, _strength);

            else
                return 0;
            
        }

        public virtual void DealDamage (int damage) { //recive damage
            _health -= damage;
        }

        public virtual void Update() // movement/update
        {
            _positon.X += _speed.X;
            _positon.Y += _speed.Y;

            if (_positon.X < 0 || _positon.X > 700)
                _speed.X = -_speed.X;
            if (_positon.Y < 0 || _positon.Y > 400)
                _speed.Y = -_speed.Y;

        }

        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_sprite, new Rectangle((int)_positon.X, (int)_positon.Y, 100, 100), Color.White); // draw the enemie
            spriteBatch.DrawString(_font, $"Health: {_health}",new Vector2(_positon.X, _positon.Y+100),Color.White); // draw the health

            spriteBatch.End();

        }
    }
}

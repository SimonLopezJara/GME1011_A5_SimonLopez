using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
/*
 * My player class
 * Here is the most simple class in this project
 * it moves, attack, get damage, has collision box, has attack collision box, has its own update and draw methods, and also change color depending on the life left.
 */
namespace GME1011_A5_SimonLopez
{
    internal class Player
    {
        private Random _rng = new Random(); // for the random values
        private int _health, _strength, _speed, _safeTime, _attackDelay; // all the ints of this project
        private Vector2 _position; // the player position
        private Texture2D _sprite; // the sprite
        private SpriteFont _font, _endScreenFont; // some fonts to print all the info
        private bool _safe; // is my player in a safe state
        private Color _color; // just  my cool color
        public Player(Vector2 position, int health, int strength, int speed, Texture2D sprite, SpriteFont font, SpriteFont endScreenFont) // constructor
        {
            _attackDelay = 0;
            _safeTime = 60;
            _color = Color.White;
            _safe = false;
            _health = health;
            _strength = strength;
            _speed = speed;
            _position = position;
            _sprite = sprite;
            _font = font;
            _endScreenFont = endScreenFont;
        }

        public bool Safe { set { _safe = value; } get { return _safe; } } // set and return if the player is safe
        public int Health { get { return _health; } } // return the health
        public int Strength { get { return _strength; } set { _strength += value; } } // return the streng and set it
        public Vector2 Position { get { return _position; } } // return player position

        public Rectangle GetBounds() { return new Rectangle((int)_position.X, (int)_position.Y, 100, 100); } // the the player collision Rectangle
    
        public void DealDamage(int damage) // Player recive damage
        {
            _health -= damage;
        }

        public int Attack() // player attack
        {
            if (_attackDelay > 0) // check if the player is in delay
            {
                return 0;
            }
            else
            {
                _attackDelay = 60;

                return _rng.Next(1, _strength);

            }
        }
        public Rectangle AttackBounds { get { return new Rectangle((int)_position.X - 50, (int)_position.Y - 50, 200, 200); } } // the attack bounds
        public void Heal(int health) // heal the hero
        {
            _health += health;
            if (_health > 100)
                _health = 100;
        }

        public void Update() { // a simple update method
            if (Keyboard.GetState().IsKeyDown(Keys.W)) //move up
                if (_position.Y > 0)
                    _position.Y -= _speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))//move down
                if (_position.Y < 400)
                    _position.Y += _speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A))//move to the left
                if (_position.X > 0)
                    _position.X -= _speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))// to the right 
                if (_position.X < 700)
                    _position.X += _speed;
            if (_safe) // safe timer 
            {
                _safeTime--;
                _color =new Color(256,256,256,120); // safe color
            }
            else
            {
                _color = new Color(256, (int)(_health * 2.56), (int)(_health * 2.56)); // life color
            }

            if (_safeTime <= 0) // remove safe state
            {
                _safe = false;
                _safeTime = 60;
            }
            if (_attackDelay > 0) // attack delay
            {
                _attackDelay--;
            }
        }
        public void Draw(SpriteBatch spriteBatch) // a simple draw method
        {
            spriteBatch.Begin();
            if (_health > 0)
            {
                spriteBatch.DrawString(_font, $"Health: {_health}",new Vector2 (_position.X+10, _position.Y+100),Color.White); // draw the life
                spriteBatch.Draw(_sprite, new Rectangle((int)_position.X, (int)_position.Y, 100, 100), _color); // draw the player
            }
            else
                spriteBatch.DrawString(_endScreenFont, "Game Over", new Vector2(190, 180), Color.White); // draw the end screen
            spriteBatch.End();
        }
    }
}

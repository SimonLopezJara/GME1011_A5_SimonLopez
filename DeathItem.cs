using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * This item kills the player when is touch
 * It disappears after 6 seconds
 * and deals 10000 of damge
 */
namespace GME1011_A5_SimonLopez
{
    internal class DeathItem : Item
    {
        private int _damage, _time;
        private SpriteFont _font;
        private Color _color;
        public DeathItem(Vector2 position, Texture2D sprite, SpriteFont font) : base (position,sprite) // Constructor and send the attributes to the parent class
        {
            _damage = 10000;
            _time = 600;
            _color = Color.White;
            _font = font;
        }

        public int Damage { get { return _damage; } } // returns the damage
        public int Time { get { return _time; } } // returns the time left
        public void Update() // my amazing update
        {
            _time--;
            _color = new Color(256, (int)(_time* 0.426), (int)(_time * 0.426)); // Change the color of the text depending on the time left
        }
        public override void Draw(SpriteBatch spriteBatch) // my super pro and advance Draw method
        {
            base.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, $"{_time}",new Vector2(_position.X,_position.Y+50),_color);
            spriteBatch.End();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * my item class, it controls the items, and also let me do a list with all my items in it.
 */
namespace GME1011_A5_SimonLopez
{
    internal class Item
    {
        protected Vector2 _position;
        protected Texture2D _sprite;
        public Item(Vector2 position, Texture2D sprite) // constructor
        {
            _position = position;
            _sprite = sprite;
        }

        public Rectangle GetBounds() { return new Rectangle((int)_position.X, (int)_position.Y, 50, 50); } // the item bounds

        public virtual void Draw(SpriteBatch spriteBatch) // the simple draw
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_sprite, GetBounds(), Color.White);

            spriteBatch.End();
        }
    }
}

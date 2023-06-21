/*
 * Simon Lopez Jaramillo
 * A00279010
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GME1011_A5_SimonLopez
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random _rng;
        private Player _player;
        private List<Enemie> _enemies;
        private List<Item> _items;
        private Texture2D _healthPoti,_attackPoti,_deathItem,_room,_vladSpr,_zombieSpr;
        private SpriteFont _InfoFont, _font;
        private int _itemTimer, _spawnDelay, _tutoTimer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800; // change the window size
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
        }

        protected override void Initialize() // initialize the important attributes
        {
            _rng = new Random();
            _enemies = new List<Enemie>();
            _items = new List<Item>();
            _itemTimer = 180;
            _spawnDelay = 600;
            _tutoTimer = 300;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load the player sprite and initialize the player
            _healthPoti = Content.Load<Texture2D>("Potion-of-Healing_64_NS"); // Load the content
            _attackPoti = Content.Load<Texture2D>("attack");
            _deathItem = Content.Load<Texture2D>("frame-1");
            _InfoFont = Content.Load<SpriteFont>("PlayerInfo");
            _room = Content.Load<Texture2D>("room");
            _vladSpr = Content.Load<Texture2D>("bat-1");
            _zombieSpr = Content.Load<Texture2D>("Zombie");
            _font = Content.Load<SpriteFont>("File"); // yes im too lazy to change the name of the file
            _player = new Player( // create the player
                new Vector2(300, 300),
                100,
                10,
                3,
                Content.Load<Texture2D>("player-idle1"),
                _InfoFont,
                Content.Load<SpriteFont>("EndScreen")
            );
            for (int i = 0; i < 2; i++) { // add the firts 2 enemies
                CreateEnemie();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(); // update my player so i can move

            foreach (var enemy in _enemies) // update the enemies
            {
                enemy.Update();
                if (enemy.Bounds.Intersects(_player.GetBounds())) // check if the enemy is intersecting with the player
                {
                    if (!_player.Safe) // check if the player is not safe
                    {
                        if (_rng.NextDouble() < .8) // run a chance to check if the attack is normal or special
                            _player.DealDamage(enemy.Attack());
                        else
                        {
                            if (enemy.GetType() == typeof(Vlad)) // special attack from vlad 
                                _player.DealDamage(((Vlad)enemy).BloodExtration());
                            if (enemy.GetType() == typeof(Zombie)) // special attack from the zombie  
                                _player.DealDamage(((Zombie)enemy).Bite());
                        }
                        _player.Safe = true; // make the player safe
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) // the player attack?
                {
                    if (_player.AttackBounds.Intersects(enemy.Bounds)) // is there a enemy on the range?
                    {
                        enemy.DealDamage(_player.Attack());
                    }
                }
            }

            for (int i=0; i < _enemies.Count; i++) // remove enemies if the health is less or == to 0
            {
                if (_enemies[i].Health <= 0)
                    _enemies.RemoveAt(i);
            }


            for (int i = 0; i < _items.Count; i++) 
            {
                var item = _items[i];
                if (item.GetType() == typeof(DeathItem)) // just a non lethal item :)
                {
                    ((DeathItem)item).Update(); // update the NonLethal item ¿)
                }
                if (_player.GetBounds().Intersects(item.GetBounds())) // collect items 
                {
                    if (item.GetType() == typeof(HealthPotion))
                        _player.Heal(((HealthPotion)item).Health);
                    if (item.GetType() == typeof(AttackPotion))
                        _player.Strength = ((AttackPotion)item).Attack;
                    if (item.GetType() == typeof(DeathItem))
                        _player.DealDamage(((DeathItem)item).Damage);

                    _items.RemoveAt(i);
                }
                if (item.GetType() == typeof(DeathItem)) // remove the NonLethal item when the time is over
                    if (((DeathItem)item).Time <= 0)
                        _items.RemoveAt(i);


            }

            if (_itemTimer <=0) // create new items every x seconds
            {
                double chance = _rng.NextDouble();
                Debug.WriteLine(chance.ToString());
                if (chance <= .4)
                    _items.Add(new HealthPotion(new Vector2(_rng.Next(0,750), _rng.Next(0, 450)), _healthPoti, _rng.Next(10,50)));
                else if (chance <= .8)
                    _items.Add(new AttackPotion(new Vector2(_rng.Next(0, 750), _rng.Next(0, 450)), _attackPoti, _rng.Next(1, 4)));
                else
                    _items.Add(new DeathItem(new Vector2(_rng.Next(0, 750), _rng.Next(0, 450)), _deathItem, _InfoFont));
                _itemTimer = _rng.Next(180, 600);

                Debug.WriteLine("Item has spawn");
            }
            else // update my timer
            {
                _itemTimer--;
            }

            if(_spawnDelay <= 0) // spawn new enemies
            {
                CreateEnemie();
                _spawnDelay = _rng.Next(250, 500);
            }
            else //update my spawn delay
            {
                _spawnDelay--;
            }
            if (_tutoTimer > 0)
                _tutoTimer--;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_room, new Rectangle(0, 0, 800, 500), Color.White); // draw the background
            _spriteBatch.End();

            foreach (var enemy in _enemies) // draw the enemies
                enemy.Draw(_spriteBatch);
            

            foreach (var item in _items) // draw the items
                item.Draw(_spriteBatch);

            _player.Draw(_spriteBatch); // draw the player 

            if (_tutoTimer > 0) // draw the tutorial just for a few seconds
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_font, "Use WASD to move and Space to attack", new Vector2(10, 10), Color.White);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
        protected void CreateEnemie () // create new enemies
        {
            if(_rng.NextDouble() < .5)
            {
                _enemies.Add(new Vlad(
                    new Vector2(_rng.Next(0, 700), _rng.Next(0, 400)),
                    20,
                    _rng.Next(1, 20),
                    new Vector2(_rng.Next(2, 6), _rng.Next(2, 6)),
                    _vladSpr,
                    _InfoFont
                    ));
            }
            else
            {
                _enemies.Add(new Zombie(
                    new Vector2(_rng.Next(0, 700), _rng.Next(0, 400)),
                    70,
                    _rng.Next(2, 10),
                    new Vector2(_rng.Next(1, 2), _rng.Next(1, 2)),
                    _zombieSpr,
                    _InfoFont
                    ));
            }
        }
    }
}
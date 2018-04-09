using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Models;
using Pong.Sprites;
using System;
using System.Collections.Generic;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;
        public static Random random;

        private Score _score;
        private List<Sprite> _sprites;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var batTexture = Content.Load<Texture2D>("Bat");
            var ballTexture = Content.Load<Texture2D>("Ball");
            _score = new Score(Content.Load<SpriteFont>("Font"));
            _sprites = new List<Sprite>()
            {
                new Sprite(Content.Load<Texture2D>("Background")),
                new Bat(batTexture)
                {
                    Position = new Vector2(20,(ScreenHeight /2) - (batTexture.Height/2)),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                    }
                },
                 new Bat(batTexture)
                {
                    Position = new Vector2(ScreenWidth - 20 - batTexture.Width,(ScreenHeight /2) - (batTexture.Height/2)),
                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                    }
                },
                 new Ball(ballTexture)
                 {
                     Position =  new Vector2((ScreenWidth/2)- (ballTexture.Width /2), (ScreenHeight / 2) - (ballTexture.Height/2)),
                     Score = _score,
                 }
            };
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }
            _score.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
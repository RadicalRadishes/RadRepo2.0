using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Common;

namespace Pong.Sprites
{
    public class Ball : DrawableGameComponent
    {
        private float _timer = 0f; // increase speed over time
        private Vector2? _startPosition = null;
        private float? _startSpeed;
        private bool _isPlaying;

        public Texture2D ballTexture;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Speed;

        public Score Score;
        public int SpeedIncrementSpan = 10; // How often the speed with increment
        BallData ballData;


        public Ball(Game game, BallData bData ) : base(game)
        {
            Speed = 3f;
            ballData = bData;
            ballTexture = Game1.ballTexture;
            Position = new Vector2((Game1.ScreenWidth / 2) - (ballTexture.Width / 2), (Game1.ScreenHeight / 2) - (ballTexture.Height / 2));


        }

        public override void Update(GameTime gameTime)
        {
            if (_startPosition == null)
            {
                _startPosition = Position;
                _startSpeed = Speed;
                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _isPlaying = true;

            if (!_isPlaying)
                return;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > SpeedIncrementSpan)
            {
                Speed++;
                _timer = 0;
            }

            //foreach (var sprite in sprites)
            //{
            //    if (sprite == this)
            //        continue;

            //    if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
            //        this.Velocity.X = -this.Velocity.X;
            //    if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
            //        this.Velocity.X = -this.Velocity.X;
            //    if (this.Velocity.Y < 0 && this.IsTouchingTop(sprite))
            //        this.Velocity.Y = -this.Velocity.Y;
            //    if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
            //        this.Velocity.Y = -this.Velocity.Y;
            //}

            if (Position.Y <= 0 || Position.Y + ballTexture.Height >= Game1.ScreenHeight)
                Velocity.Y = -Velocity.Y;

            if (Position.X <= 0)
            {
                Score.Score1++;
                Restart();
            }

            if (Position.X + ballTexture.Width >= Game1.ScreenWidth)
            {
                Score.Score2++;
                Restart();
            }

            Position += Velocity * Speed;
        }

        public void Restart()
        {
            var direction = Game1.random.Next(0, 4);

            //direction ball moves in
            switch (direction)
            {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;

                case 1:
                    Velocity = new Vector2(1, -1);
                    break;

                case 2:
                    Velocity = new Vector2(-1, -1);
                    break;

                case 3:
                    Velocity = new Vector2(-1, 1);
                    break;
            }

            Position = (Vector2)_startPosition;
            Speed = (float)_startSpeed;
            _timer = 0;
            _isPlaying = false;
        }
    }
}
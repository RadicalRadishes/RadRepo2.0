using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SingleClient.Sprites
{
    public class Ball : Sprite
    {
        private float _timer = 0f; // increase speed over time
        private Vector2? _startPosition = null;
        private float? _startSpeed;
        private bool _isPlaying;

        public Score Score;
        public int SpeedIncrementSpan = 10; // How often the speed will increase

        public Ball(Texture2D texture) : base(texture)
        {
            Speed = 3f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (_startPosition == null)
            {
                _startPosition = Position;
                _startSpeed = Speed;
                Restart(); // Pick direction to travel
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _isPlaying = true;

            if (!_isPlaying)
                return;

            //Increase speed every x seconds
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > SpeedIncrementSpan)
            {
                Speed++;
                _timer = 0;
            }

            //Check if intersects
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.Y < 0 && this.IsTouchingTop(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
                if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
            }

            if (Position.Y <= 0 || Position.Y + _texture.Height >= Game1.ScreenHeight)
                Velocity.Y = -Velocity.Y;

            //If goes off screen Left, increase player2's score
            if (Position.X <= 0)
            {
                Score.Score2++;
                Restart();
            }

            //If goes off screen Right, increase player1's score
            if (Position.X + _texture.Width >= Game1.ScreenWidth)
            {
                Score.Score1++;
                Restart();
            }

            Position += Velocity * Speed;
        }

        public void Restart()
        {
            var direction = Game1.random.Next(0, 4);

            //Pick random starting direction to 'bounce' to
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
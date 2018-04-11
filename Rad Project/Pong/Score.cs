using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Score
    {
        public int Score1;
        public int Score2;

        private SpriteFont _font;

        public Score(SpriteFont sFont)
        {
            _font = sFont;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Score1.ToString(), new Vector2(320, 70), Color.White);
            spriteBatch.DrawString(_font, Score2.ToString(), new Vector2(430, 70), Color.White);
        }
    }
}
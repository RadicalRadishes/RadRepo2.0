using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Common;

namespace Pong.Sprites
{
    public class OtherPlayerSprite : DrawableGameComponent
    {

        //Variables
        public Texture2D Image;

        public Point Position;
        public Point Target;
        public Rectangle BoundingRect;

        public TimeSpan delay = new TimeSpan(0, 0, 1);
        //public bool Visible = true;
        public PlayerData pData;

        //For Bounding Rect
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            }
        }

        public OtherPlayerSprite(Game game, PlayerData data, Texture2D _texture, Point startPosition) : base(game)
        {

            pData = data;
            game.Components.Add(this);
            Image = Game1.playerTexture;
            BoundingRect = new Rectangle(startPosition.X, startPosition.Y, Image.Width, Image.Height);

            //Position = playernumberpos;
            Target = startPosition;

        }

        public override void Update(GameTime gameTime)
        {

            if (Vector2.Distance(Position.ToVector2(), Target.ToVector2()) > 0.1f)
            {
                Position = Vector2.Lerp(Position.ToVector2(), Target.ToVector2(), 0.1f).ToPoint();
            }
            else
            {
                Position = Target;
            }

            BoundingRect = new Rectangle(Position.X, Position.Y, Image.Width, Image.Height);
            base.Update(gameTime);

        }




        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sp = Game.Services.GetService<SpriteBatch>();
            if (sp == null) return;
            if (Image != null && Visible)
            {
                sp.Begin();
                sp.Draw(Image, BoundingRect, Color.White);
                sp.End();
            }

            //spriteBatch.Draw(Image, BoundingRect, Color.White);

            base.Draw(gameTime);


        }

        //#region Collision

        //protected bool IsTouchingLeft(Sprite sprite)
        //{
        //    return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
        //        this.Rectangle.Left < sprite.Rectangle.Left &&
        //        this.Rectangle.Bottom > sprite.Rectangle.Top &&
        //        this.Rectangle.Top < sprite.Rectangle.Bottom;
        //}

        //protected bool IsTouchingRight(Sprite sprite)
        //{
        //    return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
        //        this.Rectangle.Right > sprite.Rectangle.Right &&
        //        this.Rectangle.Bottom > sprite.Rectangle.Top &&
        //        this.Rectangle.Top < sprite.Rectangle.Bottom;
        //}

        //protected bool IsTouchingTop(Sprite sprite)
        //{
        //    return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
        //        this.Rectangle.Top < sprite.Rectangle.Top &&
        //        this.Rectangle.Right > sprite.Rectangle.Left &&
        //        this.Rectangle.Left < sprite.Rectangle.Right;
        //}

        //protected bool IsTouchingBottom(Sprite sprite)
        //{
        //    return this.Rectangle.Top + this.Velocity.Y > sprite.Rectangle.Bottom &&
        //        this.Rectangle.Bottom < sprite.Rectangle.Bottom &&
        //        this.Rectangle.Right > sprite.Rectangle.Left &&
        //        this.Rectangle.Left < sprite.Rectangle.Right;
        //}

        //#endregion Collision

    }
}

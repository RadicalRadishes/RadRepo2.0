using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using Common;
using Pong.Models;

namespace Pong.Sprites
{
    public class PlayerSprite : DrawableGameComponent
    {
        //Variables
        public Vector2 Velocity;
        public float Speed;
        public Input input;
        public Texture2D Image;

        public Point Position;
        public Point PrePosition;
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


        public PlayerSprite(Game game, PlayerData data,Texture2D texture, Point startPosition)
          : base(game)
        {
            pData = data;
            DrawOrder = 1;
            game.Components.Add(this);

            Image = texture;

            BoundingRect = Rectangle;


        }
        public override void Update(GameTime gameTime)
        {
            //Add Input
            //Refer to casualGames Update

            delay -= gameTime.ElapsedGameTime;

            //To Update playerPosition
            if (Position != PrePosition)
            {
                delay = new TimeSpan(0, 0, 1);

                pData.playerPosition = new Position { X = Position.X, Y = Position.Y};
                IHubProxy proxy = Game.Services.GetService<IHubProxy>();// Gets the Proxy from the services subscription
                proxy.Invoke("Moved", new Object[]
                    {
                        pData.playerID,
                        pData.playerPosition
                    });

            }

            //Change position of bonding Rect
            BoundingRect = new Rectangle(Position.X,Position.Y,Image.Width,Image.Height);

            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sp = Game.Services.GetService<SpriteBatch>();
            if (sp == null) return;
            if(Image != null && Visible)
            {
                sp.Begin();
                sp.Draw(Image, BoundingRect, Color.White);
                sp.End();
            }

            //spriteBatch.Draw(Image, BoundingRect, Color.White);

            base.Draw(gameTime);


        }

        #region Collision

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Left &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                this.Rectangle.Right > sprite.Rectangle.Right &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Top &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y > sprite.Rectangle.Bottom &&
                this.Rectangle.Bottom < sprite.Rectangle.Bottom &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion Collision


    }
}

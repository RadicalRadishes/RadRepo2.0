using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Models;
using Pong.Sprites;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;

using Common;

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
        private Sprite backgroundSprite;

        //Imagery Variables
        public static Texture2D ballTexture;
        public static Texture2D playerTexture;

        //Connection Variables
        HubConnection serverConnection;
        IHubProxy proxy;
        public bool Connected { get; private set; }
        PlayerData clientPlayer;



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

            //hosting locally 
            serverConnection = new HubConnection("https://rad2assignment.azurewebsites.net");
            serverConnection.StateChanged += ServerConnection_StateChanged;
            proxy = serverConnection.CreateHubProxy("GameHub");
            serverConnection.Start();

            Action<PlayerData> joined = clientJoined;
            proxy.On<PlayerData>("Joined", joined);

            Action<List<PlayerData>> currentPlayers = clientPlayers;
            proxy.On<List<PlayerData>>("CurrentPlayers", currentPlayers);

            Action<string, Position> otherMove = clientOtherMoved;
            proxy.On<string, Position>("OtherMove", otherMove);

            //gets called by proxy to send leftmessage
            Action<PlayerData> left = LeaveGame;
            proxy.On<PlayerData>("Left", left);


            Services.AddService<IHubProxy>(proxy);


            base.Initialize();
        }

        #region SignalR 

        private void ServerConnection_StateChanged(StateChange State)
        {
            switch (State.NewState)
            {
                case ConnectionState.Connected:
                    Connected = true;
                    startGame();
                    break;
                case ConnectionState.Disconnected:
                    if (State.OldState == ConnectionState.Connected)
                        Connected = false;
                    break;
                case ConnectionState.Connecting:
                    Connected = false;
                    break;

            }
        }



        private void startGame()
        {
            // Continue on and subscribe to the incoming messages joined, currentPlayers, otherMove messages

            // Immediate Pattern
            proxy.Invoke<PlayerData>("Join")
                .ContinueWith( // This is an inline delegate pattern that processes the message 
                               // returned from the async Invoke Call
                        (p) => { // Wtih p do 

                            CreatePlayer(p.Result);
                            // Here we'll want to create our game player using the image name in the PlayerData 
                            // Player Data packet to choose the image for the player
                            // We'll use a simple sprite player for the purposes of demonstration                           

                        });
        }



        private void clientJoined(PlayerData otherPlayerData)
        {
            // Create another player sprite
            //use points in game to make movement smoother
            new OtherPlayerSprite(this, otherPlayerData, playerTexture,
                                    new Point(otherPlayerData.playerPosition.X, otherPlayerData.playerPosition.Y));
        }



        private void clientPlayers(List<PlayerData> otherPlayers)
        {
            foreach (PlayerData player in otherPlayers)
            {
                // Create an other player sprites in this client after
                new OtherPlayerSprite(this, player, playerTexture,
                                        new Point(player.playerPosition.X, player.playerPosition.Y));
            }
        }



        private void clientOtherMoved(string playerID, Position newPos)
        {
            // iterate over all the other player components 
            // and check to see the type and the right id
            foreach (var player in Components)
            {
                if (player.GetType() == typeof(OtherPlayerSprite)
                    && ((OtherPlayerSprite)player).pData.playerID == playerID)
                {
                    OtherPlayerSprite p = ((OtherPlayerSprite)player);
                    p.pData.playerPosition = newPos;
                    p.Position = new Point(p.pData.playerPosition.X, p.pData.playerPosition.Y);
                    break; // break out of loop as only one player position is being updated
                           // and we have found it
                }
            }
        }



        private void LeaveGame(PlayerData playdata)
        {

        }


        #endregion


        #region ClientSpecific

        private void CreatePlayer(PlayerData player)
        {
            new PlayerSprite(this, player, Content.Load<Texture2D>(player.imageName),
                new Point(player.playerPosition.X, player.playerPosition.Y));
            clientPlayer = player;

            //To display coins on Load
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load Textures and Font
            playerTexture = Content.Load<Texture2D>("Bat");
            ballTexture = Content.Load<Texture2D>("Ball");
            _score = new Score(Content.Load<SpriteFont>("Font"));

            backgroundSprite = new Sprite(this, Content.Load<Texture2D>("Background"), new Point((ScreenHeight / 2), (ScreenWidth / 2)));
            //_sprites = new List<Sprite>()
            //{
            //    new Sprite(Content.Load<Texture2D>("Background")),
            //    new Bat(batTexture)
            //    {
            //        Position = new Vector2(20,(ScreenHeight /2) - (batTexture.Height/2)),
            //        Input = new Input()
            //        {
            //            Up = Keys.W,
            //            Down = Keys.S,
            //        }
            //    },
            //     new Bat(batTexture)
            //    {
            //        Position = new Vector2(ScreenWidth - 20 - batTexture.Width,(ScreenHeight /2) - (batTexture.Height/2)),
            //        Input = new Input()
            //        {
            //            Up = Keys.Up,
            //            Down = Keys.Down,
            //        }
            //    },
            //     new Ball(ballTexture)
            //     {
            //         Position =  new Vector2((ScreenWidth/2)- (ballTexture.Width /2), (ScreenHeight / 2) - (ballTexture.Height/2)),
            //         Score = _score,
            //     }
            //};

        }



        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {

                proxy.Invoke<PlayerData>("LeftGame", clientPlayer).ContinueWith( // This is an inline delegate pattern that processes the message 
                                                                                 // returned from the async Invoke Call
          (p) => {
              LeaveGame(clientPlayer);
          });


                Exit();
            }
            //foreach (var sprite in _sprites)
            //{
            //    sprite.Update(gameTime, _sprites);
            //}

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //foreach (Sprite sprite in _sprites)
            //{
            //    sprite.Draw(spriteBatch);
            //}
            backgroundSprite.Draw(spriteBatch);
            _score.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion
    }
}
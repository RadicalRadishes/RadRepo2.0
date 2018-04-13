using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Microsoft.AspNet.SignalR;


namespace API
{
    public class Gamehub : Hub
    {
        public static int Player1 = 1;
        public static int Player2 = 2;


        public static List<PlayerData> players = new List<PlayerData>();

        public BallData ball;


        public PlayerData Join()
        {

            //Add in log in authentication



            if (players.Count < 2)
            {
                PlayerData newPlayer = new PlayerData();
                //Sets ID, used to determine if on the Left side or Right side
                newPlayer.playerID = (players.Count + 1).ToString();
                if (newPlayer.playerID == "1")
                {
                    newPlayer.playerPosition = new Position
                    {

                        X = 20,
                        Y = 150
                    };
                }
                else
                {
                    newPlayer.playerPosition = new Position
                    {

                        X = 100,
                        Y = 150
                    };
                }

                // Tell all the other clients that this player has Joined
                Clients.Others.Joined(newPlayer);
                // Tell this client about all the other current 
                Clients.Caller.CurrentPlayers(players);
                // add the new player on the server
                players.Add(newPlayer);
                return newPlayer;
            }

            return null;

        } // End of Join



        public void Moved(string playerID, Position newPosition)
        {
            // Update the collection with the new player position is the player exists
            PlayerData found = players.FirstOrDefault(p => p.playerID == playerID);

            if (found != null)
            {
                // Update the server player position
                found.playerPosition = newPosition;
                // Tell all the other clients this player has moved
                Clients.Others.OtherMove(playerID, newPosition);
            }
        }

        public void Bounce(BallData _ballData)
        {

        }

        public void LeftGame(PlayerData pdata)
        {

            Clients.Others.Left(pdata); // Calls the Action<PlayerData> left in the client
            players.Remove(pdata); // remove from players on server data


        }

        public void SpawnBall()
        {

        }



    }



}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Position
    {
        public int X;
        public int Y;
    }

    public class PlayerData
    {
        public string playerID;
        public string imageName;
        public string GamerTag = string.Empty;
        public string PlayerName = string.Empty;
        public Position playerPosition;
        public string Password = string.Empty;
        public int Score;
        public int highScore;
    }

    public class BallData
    {
        public string imageNae = string.Empty;
        public Position ballPos;
        public Position VelocityDirection;
    }
}

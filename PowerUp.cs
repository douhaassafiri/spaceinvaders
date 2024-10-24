using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class PowerUp
    {
        // Fields
        private Vector2D _position;
        private bool _isCollected;
        private string _type;
        private const int _size = 50;

        // Properties
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle()
                {
                    X = _position.X,
                    Y = _position.Y,
                    Width = _size,
                    Height = _size
                };
            }
        }

        public bool IsCollected
        {
            get { return _isCollected; }
            set { _isCollected = value; }
        }

        public string Type
        {
            get { return _type; }
        }

        // Constructors
        public PowerUp(string type, float x, float y)
        {
            _type = type;
            _position = new Vector2D() { X = x, Y = y };
            _isCollected = false;
        }

        // Methods
        public void Move()
        {
            _position.Y += 1;
        }

        public void DrawPowerUp()
        {
            Color color = Type == "AOE" ? Color.Blue : Color.Green;
            DrawStar(color, (float)_position.X, (float)_position.Y, _size);
        }

        public void DrawStar(Color color, float centerX, float centerY, float size)
        {
            float x1 = centerX;
            float x2 = centerX - size / 2 * (float)Math.Sin(2 * Math.PI / 5);
            float x3 = centerX + size / 2 * (float)Math.Sin(4 * Math.PI / 5);
            float x4 = centerX - size / 2 * (float)Math.Sin(4 * Math.PI / 5);
            float x5 = centerX + size / 2 * (float)Math.Sin(2 * Math.PI / 5);
            float x6 = centerX;

            float y1 = centerY - size / 2;
            float y2 = centerY - size / 2 * (float)Math.Cos(2 * Math.PI / 5);
            float y3 = centerY - size / 2 * (float)Math.Cos(4 * Math.PI / 5);
            float y4 = centerY - size / 2 * (float)Math.Cos(4 * Math.PI / 5);
            float y5 = centerY - size / 2 * (float)Math.Cos(2 * Math.PI / 5);
            float y6 = centerY + (size / 2) - (float)(0.30 * size);

            SplashKit.FillTriangle(color, x1, y1, x3, y3, x4, y4);
            SplashKit.FillTriangle(color, x2, y2, x5, y5, x6, y6);
            SplashKit.FillTriangle(Color.Black, x3, y3, x4, y4, x6, y6);
        }

        public void Collect(PlayerShip player, ScoreManager scoreManager)
        {
            if (_type == "AOE")
            {
                player.EnableAOEShooting();
            }
            else if (_type == "Lives")
            {
                player.AddLife();
            }

            scoreManager.PowerUpCollected();

            IsCollected = true;
        }
    }
}

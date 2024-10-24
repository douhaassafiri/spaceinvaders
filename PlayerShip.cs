using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class PlayerShip : Entity, IShooter
    {
        // Fields
        private int _lives;
        private int _hitCount;
        private const int _maxHitsBeforeLifeLoss = 100;
        private const int _windowWidth = 800;
        private const int _windowHeight = 600;
        private bool _aoeShootingEnabled;
        private int _bulletsPerShot = 1; 

        // Properties
        public int Lives => _lives;
        public string Weapon => $"{_bulletsPerShot}";

        // Constructors
        public PlayerShip(float x, float y) : base(x, y, 100)
        {
            _lives = 5;
            _hitCount = 0;
            _aoeShootingEnabled = false;
        }

        // Methods
        public override void Move(float speed)
        {
            Vector2D newPosition = Position;

            if ((SplashKit.KeyDown(KeyCode.LeftKey) || SplashKit.KeyDown(KeyCode.AKey)) && newPosition.X > 20)
            {
                newPosition.X -= speed;
            }

            if ((SplashKit.KeyDown(KeyCode.RightKey) || SplashKit.KeyDown(KeyCode.DKey)) && newPosition.X < _windowWidth - 20)
            {
                newPosition.X += speed;
            }

            if ((SplashKit.KeyDown(KeyCode.UpKey) || SplashKit.KeyDown(KeyCode.WKey)) && newPosition.Y > 50)
            {
                newPosition.Y -= speed;
            }

            if ((SplashKit.KeyDown(KeyCode.DownKey) || SplashKit.KeyDown(KeyCode.SKey)) && newPosition.Y < _windowHeight - 100)
            {
                newPosition.Y += speed;
            }

            Position = newPosition;
        }

        public override void Shoot(List<Projectile> projectiles)
        {
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                for (int i = 0; i < _bulletsPerShot; i++)
                {
                    float offsetX = (i - (_bulletsPerShot - 1) / 2.0f) * 10;
                    projectiles.Add(new StandardBullet((float)Position.X + offsetX, (float)Position.Y));
                }
            }
        }

        public void EnableAOEShooting()
        {
            _aoeShootingEnabled = true;
            _bulletsPerShot += 2;
        }

        public override void OnHit(int damage)
        {
            _hitCount += damage;

            if (_hitCount >= _maxHitsBeforeLifeLoss)
            {
                _hitCount = 0;
                _lives--;

                if (_lives <= 0)
                {
                    Destroy();
                }
            }
        }

        public void ReplenishLives()
        {
            _lives = 5;
        }

        public void AddLife()
        {
            _lives++;
        }

        public override void DrawEntity()
        {
            SplashKit.FillTriangle(GetEntityColor(Color.White), (float)Position.X, (float)Position.Y, (float)Position.X - 20f, (float)Position.Y + 30f, (float)Position.X + 20f, (float)Position.Y + 30f);
        }

        public void DrawLives()
        {
            float startX = 470;
            float startY = 15;
            float spacing = 20;
            float size = 5;

            for (int i = 0; i < _lives; i++)
            {
                DrawHeart(startX + i * spacing, startY, size);
            }
        }

        public void DrawHeart(float x, float y, float size)
        {
            Color heartColor = Color.Red;

            SplashKit.FillTriangle(heartColor, x, y + size, x - size, y, x + size, y );
            SplashKit.FillTriangle(heartColor, x - size, y, x - (size / 2), y - size, x, y);
            SplashKit.FillTriangle(heartColor, x, y, x + (size / 2), y - size, x + size, y);
        }
    }
}

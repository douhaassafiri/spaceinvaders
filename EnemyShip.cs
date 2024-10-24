using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class EnemyShip : Entity, IShooter
    {
        // Fields
        private EnemyFactory _enemyFactory;
        private int _health;
        private int _points;
        private string _type;

        // Properties
        public int Points
        {
            get { return _points; }
        }

        // Constructors
        public EnemyShip(float x, float y, string type, EnemyFactory factory) : base(x, y, 2)
        {
            _enemyFactory = factory;
            _type = type;

            switch (type)
            {
                case "boss":
                    _health = 20;
                    _points = 500;
                    break;
                case "chaser":
                    _health = 5;
                    _points = 200;
                    break;
                default:
                    _health = 2;
                    _points = 100;
                    break;
            }
        }

        // Methods
        public override void Move(float speed)
        {
            Vector2D newPosition = Position;
            newPosition.X += speed;
            Position = newPosition;
        }

        public override void Shoot(List<Projectile> projectiles)
        {
            projectiles.Add(new AlienLaser(Position.X, Position.Y + 10));
        }

        public override void OnHit(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _health = 0;
                Destroy();
            }
        }

        public override void DrawEntity()
        {
            SplashKit.FillRectangle(GetEntityColor(Color.Gray), Position.X, Position.Y, 20, 20);
        }

        public override void Destroy()
        {
            if (!IsDestroyed)
            {
                base.Destroy();
            }
        }
    }
}

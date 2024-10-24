using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class StandardBullet : Projectile
    {
        // Constructors
        public StandardBullet(float x, float y) : base(x, y, 5.0f, 1, 5, 10)
        {
            _speed = 5.0f;
            _damage = 1;
        }

        // Methods
        public override void Move()
        {
            Vector2D newPosition = Position;
            newPosition.Y -= _speed;
            Position = newPosition;
        }

        public override void OnHit(Entity target)
        {
            target.OnHit(_damage);
        }

        public override void DrawProjectile()
        {
            SplashKit.FillRectangle(Color.Yellow, Position.X, Position.Y, 5, 10);
        }
    }
}

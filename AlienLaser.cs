using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class AlienLaser : Projectile
    {
        // Constructors
        public AlienLaser(double x, double y) : base((float)x, (float)y, 3.0f, 10, 5, 10)
        {
        }

        // Methods
        public override void Move()
        {
            Vector2D newPosition = Position;
            newPosition.Y += _speed;
            Position = newPosition;
        }

        public override void DrawProjectile()
        {
            SplashKit.FillRectangle(Color.Green, Position.X, Position.Y, 5, 10);
        }

        public override void OnHit(Entity target)
        {
            target.OnHit(_damage);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class AlienChaser : EnemyShip
    {
        // Fields
        private float _chaseSpeed;

        // Constructors
        public AlienChaser(float x, float y, EnemyFactory factory) : base(x, y, "chaser", factory)
        {
            _chaseSpeed = 3.0f;
        }

        // Methods
        public void MoveTowardsPlayer(PlayerShip player)
        {
            Vector2D direction = new Vector2D
            {
                X = player.Position.X - Position.X,
                Y = player.Position.Y - Position.Y
            };

            float magnitude = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

            if (magnitude > 0)
            {
                direction.X /= magnitude;
                direction.Y /= magnitude;
            }

            Vector2D newPosition = Position;
            newPosition.X += direction.X * _chaseSpeed;
            newPosition.Y += direction.Y * _chaseSpeed;

            Position = newPosition;
        }

        public void InflictDamageOnCollision(PlayerShip player)
        {
            if (SplashKit.RectanglesIntersect(this.BoundingBox, player.BoundingBox))
            {
                player.OnHit(1);
            }
        }

        public override void DrawEntity()
        {
            SplashKit.FillRectangle(GetEntityColor(Color.Green), Position.X, Position.Y, 20, 20);
        }
    }
}

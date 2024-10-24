using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class BossAlien : EnemyShip
    {
        // Constructors
        public BossAlien(float x, float y, EnemyFactory factory) : base(x, y, "boss", factory)
        {
        }

        // Methods
        public override void Move(float speed)
        {
            Vector2D newPosition = Position;
            newPosition.X += speed;
            Position = newPosition;
        }

        public override void DrawEntity()
        {
            SplashKit.FillRectangle(GetEntityColor(Color.Purple), Position.X, Position.Y, 50, 50);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
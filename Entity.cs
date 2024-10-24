using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public abstract class Entity
    {
        // Fields
        private Vector2D _position;
        private int _health;
        private bool _isDestroyed;

        private bool _isHit = false;
        private SplashKitSDK.Timer _hitTimer;
        private const int HitDuration = 500;

        // Properties
        public Vector2D Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public bool IsDestroyed
        {
            get { return _isDestroyed; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle()
                {
                    X = _position.X,
                    Y = _position.Y,
                    Width = 40,
                    Height = 40
                };
            }
        }

        // Constructors
        public Entity(float x, float y, int health)
        {
            _position = new Vector2D() { X = x, Y = y };
            _health = health;
            _isDestroyed = false;
            _hitTimer = new SplashKitSDK.Timer("HitTimer");
            _hitTimer.Start();
        }

        // Methods
        public abstract void Move(float speed);
        public abstract void Shoot(List<Projectile> projectiles);
        public abstract void DrawEntity();

        public virtual void OnHit(int damage)
        {
            if (damage < 0) return;

            _health -= damage;

            if (_health <= 0)
            {
                Destroy();
            }

            _isHit = true;
            _hitTimer.Reset();
        }

        public virtual void Destroy()
        {
            _isDestroyed = true;
        }

        public virtual void Update()
        {
            if (_isHit && _hitTimer.Ticks > HitDuration)
            {
                _isHit = false;
            }
        }

        public Color GetEntityColor(Color defaultColor)
        {
            return _isHit ? Color.Red : defaultColor;
        }
    }
}

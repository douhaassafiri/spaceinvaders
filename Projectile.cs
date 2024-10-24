using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public abstract class Projectile
    {
        // Fields
        protected Vector2D _position;
        protected float _speed;
        protected int _damage;
        protected int _width;
        protected int _height;
        private bool _isDestroyed;

        // Properties
        public int Damage
        {
            get { return _damage; }
        }

        public bool IsDestroyed
        {
            get { return _isDestroyed; }
        }

        public Vector2D Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle()
                {
                    X = _position.X,
                    Y = _position.Y,
                    Width = _width,
                    Height = _height
                };
            }
        }

        // Constructors
        public Projectile(float x, float y, float speed, int damage, int width, int height)
        {
            _position = new Vector2D() { X = x, Y = y };
            _speed = speed;
            _damage = damage;
            _width = width;
            _height = height;
            _isDestroyed = false;
        }

        // Methods
        public abstract void Move();
        public abstract void DrawProjectile();
        public abstract void OnHit(Entity target);

        public void Destroy()
        {
            _isDestroyed = true;
        }
    }
}
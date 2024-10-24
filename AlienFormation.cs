using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class AlienFormation
    {
        // Fields
        private List<EnemyShip> _enemies;
        private bool _movingRight = true;
        private bool _isPaused = false;
        private bool _isShooting = true;
        private Random _random = new Random();

        private int _moveSteps = 0;
        private int _pauseSteps = 0;
        private int _shootSteps = 0;
        private int _pauseShootSteps = 0;

        private const int _moveLimit = 60;
        private const int _pauseLimit = 40;
        private const int _shootLimit = 1;
        private const int _pauseShootLimit = 18;
        private const int WindowWidth = 800;
        private const float Speed = 1.0f;

        // Properties
        public List<EnemyShip> Enemies
        {
            get { return _enemies; }
        }

        // Constructors
        public AlienFormation(int rows, int cols, EnemyFactory factory)
        {
            _enemies = new List<EnemyShip>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    EnemyShip enemy = factory.CreateEnemy("basic", 50 + col * 50, 50 + row * 50);
                    _enemies.Add(enemy);
                }
            }
        }

        // Methods
        public void AddEnemy(EnemyShip enemy)
        {
            _enemies.Add(enemy);
        }

        public void MoveAndShoot(List<Projectile> projectiles)
        {
            if (_isPaused)
            {
                HandleMovementPause();
                HandleShootingCycle(projectiles);
            }
            else
            {
                MoveFormation();
            }
        }

        public void MoveFormation()
        {
            float movementSpeed = _movingRight ? Speed : -Speed;

            foreach (var enemy in _enemies)
            {
                enemy.Move(movementSpeed);
            }

            _moveSteps++;

            if (_moveSteps >= _moveLimit)
            {
                _isPaused = true;
                _moveSteps = 0;
            }

            CheckForBoundaryHit();
        }

        public void CheckForBoundaryHit()
        {
            if (_movingRight && _enemies.Any(e => e.Position.X + 40 >= WindowWidth))
            {
                _movingRight = false;
            }
            else if (!_movingRight && _enemies.Any(e => e.Position.X <= 0))
            {
                _movingRight = true;
            }
        }

        public void HandleMovementPause()
        {
            _pauseSteps++;

            if (_pauseSteps >= _pauseLimit)
            {
                _isPaused = false;
                _pauseSteps = 0;
            }
        }

        public void HandleShootingCycle(List<Projectile> projectiles)
        {
            if (_isShooting)
            {
                _shootSteps++;
                if (_shootSteps <= _shootLimit)
                {
                    FireProjectiles(projectiles);
                }
                else
                {
                    _isShooting = false;
                    _shootSteps = 0;
                }
            }
            else
            {
                _pauseShootSteps++;
                if (_pauseShootSteps >= _pauseShootLimit)
                {
                    _isShooting = true;
                    _pauseShootSteps = 0;
                }
            }
        }

        public void FireProjectiles(List<Projectile> projectiles)
        {
            foreach (var enemy in _enemies)
            {
                if (_random.NextDouble() < 0.5)
                {
                    enemy.Shoot(projectiles);
                }
            }
        }

        public bool IsEmpty()
        {
            return _enemies.Count == 0;
        }

        public void DrawFormation()
        {
            foreach (var enemy in _enemies)
            {
                enemy.DrawEntity();
            }
        }
    }
}

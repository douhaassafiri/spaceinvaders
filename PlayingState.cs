using spaceinvaders;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceinvaders
{
    public class PlayingState : IGameState
    {
        // Fields
        private PlayerShip _player;
        private AlienFormation _alienFormation;
        private List<Projectile> _projectiles;
        private List<PowerUp> _powerUps;
        private List<AlienChaser> _alienChasers;
        private Score _score;
        private ScoreManager _scoreManager;
        private LevelManager _levelManager;
        private GameUI _gameUI;

        // Constructors
        public PlayingState(PlayerShip player, AlienFormation alienFormation, List<Projectile> projectiles, List<PowerUp> powerUps, Score score, ScoreManager scoreManager, LevelManager levelManager, List<AlienChaser> alienChasers, GameUI gameUI)
        {
            _player = player;
            _alienFormation = alienFormation;
            _projectiles = projectiles;
            _powerUps = powerUps;
            _alienChasers = alienChasers;
            _score = score;
            _scoreManager = scoreManager;
            _levelManager = levelManager;
            _gameUI = gameUI;
        }

        // Methods
        public void Handle(GameManager context)
        {
            Update(context);
            Render();
        }

        public void Update(GameManager context)
        {
            _player.Move(5);
            _player.Update();

            if (_player.IsDestroyed)
            {
                context.SetState(new GameOverState(_gameUI, _score.Value));
                return;
            }

            _alienFormation.MoveAndShoot(_projectiles);
            foreach (var chaser in _alienChasers)
            {
                chaser.MoveTowardsPlayer(_player);
                chaser.InflictDamageOnCollision(_player);
                chaser.Update();
            }

            HandlePlayerShooting();
            CheckCollisions(context);

            _alienFormation.Enemies.RemoveAll(e => e.IsDestroyed);
            _alienChasers.RemoveAll(c => c.IsDestroyed);
            _projectiles.RemoveAll(p => p.IsDestroyed);

            CheckPowerUpCollection();

            if (_alienFormation.IsEmpty() && _alienChasers.Count == 0)
            {
                ProceedToNextLevel(context);
            }
        }

        public void ProceedToNextLevel(GameManager context)
        {
            _levelManager.AdvanceLevel();
            _player.ReplenishLives();
            _powerUps.Clear();
            context.InitializeLevel();
        }


        public void HandlePlayerShooting()
        {
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                _player.Shoot(_projectiles);
            }

            foreach (var projectile in _projectiles)
            {
                projectile.Move();
            }
        }

        public void CheckCollisions(GameManager context)
        {
            foreach (var projectile in _projectiles)
            {
                if (projectile is AlienLaser && SplashKit.RectanglesIntersect(projectile.BoundingBox, _player.BoundingBox))
                {
                    _player.OnHit(projectile.Damage);
                    projectile.Destroy();
                }
                else if (projectile is StandardBullet)
                {
                    CheckEntityCollisions(projectile, _alienFormation.Enemies, context); 
                    CheckEntityCollisions(projectile, _alienChasers, context);
                }
            }
            _projectiles.RemoveAll(p => p.IsDestroyed);
        }

        public void CheckEntityCollisions(Projectile projectile, IEnumerable<EnemyShip> entities, GameManager context)
        {
            foreach (var entity in entities)
            {
                if (SplashKit.RectanglesIntersect(projectile.BoundingBox, entity.BoundingBox))
                {
                    entity.OnHit(projectile.Damage);
                    projectile.Destroy();

                    if (entity.IsDestroyed)
                    {
                        int points = entity.Points;
                        EnemyDestroyed(points);

                        if (entity is BossAlien && _levelManager.IsBossLevel())
                        {
                            context.SetState(new VictoryState(context.GameUI, _score.Value));
                            return;
                        }
                    }
                }
            }
        }


        public void EnemyDestroyed(int points)
        {
            _score.Update(points);
        }

        public void CheckPowerUpCollection()
        {
            foreach (var powerUp in _powerUps)
            {
                if (!powerUp.IsCollected && SplashKit.RectanglesIntersect(powerUp.BoundingBox, _player.BoundingBox))
                {
                    powerUp.Collect(_player, _scoreManager);
                }
            }

            _powerUps.RemoveAll(p => p.IsCollected);
        }

        public void Render()
        {
            SplashKit.ClearScreen(Color.Black);

            _player.DrawEntity();
            _alienFormation.DrawFormation();

            foreach (var chaser in _alienChasers)
            {
                chaser.DrawEntity();
            }

            foreach (var projectile in _projectiles)
            {
                projectile.DrawProjectile();
            }

            foreach (var powerUp in _powerUps)
            {
                powerUp.DrawPowerUp();
                powerUp.Move();
            }

            _gameUI.DrawUI();
        }
    }
}
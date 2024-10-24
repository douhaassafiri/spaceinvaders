using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace spaceinvaders
{
    public class GameManager
    {
        // Fields
        private IGameState _currentState;
        private IGameState _playingState;
        private IGameState _pausedState;

        private PlayerShip _player;
        private AlienFormation _alienFormation;
        private List<AlienChaser> _alienChasers;
        private List<Projectile> _projectiles;
        private List<PowerUp> _powerUps;
        private Score _score;
        private ScoreManager _scoreManager;
        private LevelManager _levelManager;

        private const int ToggleDelayFrames = 10;

        // Properties
        public PlayerShip Player
        {
            get { return _player; }
        }

        public AlienFormation AlienFormation
        {
            get { return _alienFormation; }
        }

        public List<Projectile> Projectiles
        {
            get { return _projectiles; }
        }

        public Score Score
        {
            get { return _score; }
        }

        public LevelManager LevelManager
        {
            get { return _levelManager; }
        }

        public List<AlienChaser> AlienChasers
        {
            get { return _alienChasers; }
        }

        public GameUI GameUI
        {
            get;
        }

        public int ToggleCounter
        {
            get;
            private set;
        }

        // Constructors
        public GameManager()
        {
            InitializeComponents();

            GameUI = new GameUI(_player, _score, _levelManager);

            _playingState = new PlayingState(_player, _alienFormation, _projectiles, _powerUps, _score, _scoreManager, _levelManager, _alienChasers, GameUI);
            _pausedState = new PausedState(GameUI);

            _currentState = new GameMenuState(GameUI);
        }

        // Methods
        public void InitializeComponents()
        {
            _player = new PlayerShip(400, 550);
            _projectiles = new List<Projectile>();
            _powerUps = new List<PowerUp>();
            _score = new Score();
            _scoreManager = new ScoreManager();
            _scoreManager.AddObserver(_score);
            _levelManager = new LevelManager();
            _alienChasers = new List<AlienChaser>();
            InitializeLevel();
            Console.WriteLine("New game started...");
        }

        public void SetState(IGameState state)
        {
            _currentState = state;
        }

        public void TogglePause()
        {
            if (ToggleCounter > 0) return;

            if (_currentState is PlayingState)
            {
                Console.WriteLine("Pausing game...");
                _currentState = _pausedState;
            }
            else if (_currentState is PausedState)
            {
                Console.WriteLine("Resuming game...");
                _currentState = _playingState;
            }

            ToggleCounter = ToggleDelayFrames;
        }

        public void DecrementToggleCounter()
        {
            if (ToggleCounter > 0)
            {
                ToggleCounter--;
            }
        }

        public bool IsPlaying()
        {
            return _currentState is PlayingState;
        }

        public IGameState GetPlayingState()
        {
            return _playingState;
        }

        public void GameLoop()
        {
            while (!GameUI.GameWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();

                _currentState.Handle(this);

                if (SplashKit.KeyTyped(KeyCode.EscapeKey) || MenuButtonClicked())
                {
                    TogglePause();
                }

                if (ToggleCounter > 0)
                {
                    DecrementToggleCounter();
                }

                SplashKit.RefreshScreen(60);
            }
        }

        public bool MenuButtonClicked()
        {
            return SplashKit.MouseClicked(MouseButton.LeftButton) &&
                   SplashKit.PointInRectangle(SplashKit.MousePosition(), GameUI.MenuButtonBounds);
        }

        public void RestartGame()
        {
            _projectiles.Clear();
            _alienChasers.Clear();
            _powerUps.Clear();
            _levelManager.ResetLevel();

            InitializeComponents();

            _score.Reset();

            _playingState = new PlayingState(_player, _alienFormation, _projectiles, _powerUps, _score, _scoreManager, _levelManager, _alienChasers, GameUI);

            SetState(_playingState);

            InitializeLevel();

            Console.WriteLine("Restarting game...");
        }


        public void InitializeLevel()
        {
            if (_currentState is VictoryState || _currentState is GameOverState)
            {
                return;
            }

            _alienChasers.Clear();
            _powerUps.Clear();

            EnemyFactory enemyFactory = new EnemyFactory();

            // Handle Boss level
            if (_levelManager.Level == 5)
            {
                _alienFormation = new AlienFormation(0, 0, enemyFactory);
                BossAlien boss = new BossAlien(300, 100, enemyFactory);
                _alienFormation.AddEnemy(boss);
            }
            else
            {
                (int rows, int cols) = _levelManager.GetEnemyFormationSize();
                _alienFormation = new AlienFormation(rows, cols, enemyFactory);

                int chaserCount = _levelManager.GetAlienChaserCount();
                for (int i = 0; i < chaserCount; i++)
                {
                    AlienChaser chaser = new AlienChaser(200 + (i * 100), 50, enemyFactory);
                    _alienChasers.Add(chaser);
                }
            }

            SpawnPowerUp();

            _playingState = new PlayingState(_player, _alienFormation, _projectiles, _powerUps, _score, _scoreManager, _levelManager, _alienChasers, GameUI);
            SetState(_playingState);
        }


        public void SpawnPowerUp()
        {
            Random random = new Random();
            string type = random.Next(0, 2) == 0 ? "AOE" : "Lives";
            float x = random.Next(100, 700);
            float y = 50;
            _powerUps.Add(new PowerUp(type, x, y));
        }


        public void OnEnemyDefeated(string enemyType)
        {
            _scoreManager.EnemyDefeated(enemyType);
        }

        public void OnPowerUpCollected()
        {
            _scoreManager.PowerUpCollected();
        }
    }
}

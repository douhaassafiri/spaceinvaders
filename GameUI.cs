using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class GameUI
    {
        // Fields
        private Window _gameWindow;
        private LevelManager _levelManager;
        private PlayerShip _player;
        private Score _score;
        private Rectangle _menuButtonBounds;
        private Rectangle _restartButtonBounds;
        private Rectangle _exitButtonBounds;

        // Properties
        public Window GameWindow
        {
            get { return _gameWindow; }
        }

        public LevelManager LevelManager
        {
            get { return _levelManager; }
            set { _levelManager = value; }
        }

        public PlayerShip Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Score Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public Rectangle MenuButtonBounds
        {
            get { return _menuButtonBounds; }
        }

        public Rectangle RestartButtonBounds
        {
            get { return _restartButtonBounds; }
        }

        public Rectangle ExitButtonBounds
        {
            get { return _exitButtonBounds; }
        }

        // Constructors
        public GameUI(PlayerShip player, Score score, LevelManager levelManager)
        {
            _gameWindow = new Window("Space Invaders", 800, 600);
            _player = player;
            _score = score;
            _levelManager = levelManager;

            _menuButtonBounds = new Rectangle() { X = 50, Y = 550, Width = 50, Height = 30 };
            _restartButtonBounds = new Rectangle() { X = 320, Y = 340, Width = 160, Height = 40 };
            _exitButtonBounds = new Rectangle() { X = 320, Y = 400, Width = 160, Height = 40 };
        }

        // Methods
        public void DrawUI()
        {
            _score.DisplayScore();
            SplashKit.DrawText($"Bullets: {_player.Weapon}", Color.White, "Arial", 20, 200, 10);
            SplashKit.DrawText("Lives: ", Color.White, "Arial", 20, 400, 10);

            _player.DrawLives();

            SplashKit.FillRectangle(Color.Black, _menuButtonBounds);
            SplashKit.DrawText("Menu", Color.White, "Arial", 20, _menuButtonBounds.X + 10, _menuButtonBounds.Y + 5);

            SplashKit.DrawText($"Level {_levelManager.Level}", Color.White, "Arial", 20, _gameWindow.Width - 100, 560);
        }

        public void DrawPauseMenu()
        {
            SplashKit.DrawText("PAUSED", Color.White, "Arial", 30, 350, 250);
            SplashKit.DrawText("Press ESC to Resume", Color.White, "Arial", 20, 320, 300);

            SplashKit.FillRectangle(Color.Gray, _menuButtonBounds);
            SplashKit.DrawText("Menu", Color.White, "Arial", 20, _menuButtonBounds.X + 10, _menuButtonBounds.Y + 5);

            SplashKit.FillRectangle(Color.Gray, _restartButtonBounds);
            SplashKit.DrawText("Restart", Color.White, "Arial", 20, _restartButtonBounds.X + 10, _restartButtonBounds.Y + 10);

            SplashKit.FillRectangle(Color.Gray, _exitButtonBounds);
            SplashKit.DrawText("Exit", Color.White, "Arial", 20, _exitButtonBounds.X + 10, _exitButtonBounds.Y + 10);
        }

        public void DrawMenu()
        {
            SplashKit.DrawText("Press ENTER to Start", Color.White, "Arial", 30, 350, 300);
            SplashKit.DrawText("Space Invaders", Color.White, "Arial", 40, 350, 200);
        }

        public void DrawGameOverScreen(int score)
        {
            SplashKit.DrawText("YOU LOSE!", Color.White, 350, 200);
            SplashKit.DrawText($"Your Score: {score}", Color.White, 350, 250);

            SplashKit.FillRectangle(Color.Gray, _restartButtonBounds);
            SplashKit.DrawText("Restart", Color.White, "Arial", 20, _restartButtonBounds.X + 10, _restartButtonBounds.Y + 10);

            SplashKit.FillRectangle(Color.Gray, _exitButtonBounds);
            SplashKit.DrawText("Exit", Color.White, "Arial", 20, _exitButtonBounds.X + 10, _exitButtonBounds.Y + 10);
        }

        public void DrawVictoryScreen(int score)
        {
            SplashKit.DrawText("YOU WIN!", Color.White, 350, 200);
            SplashKit.DrawText($"Your Score: {score}", Color.White, 350, 250);

            SplashKit.FillRectangle(Color.Gray, _restartButtonBounds);
            SplashKit.DrawText("Restart", Color.White, "Arial", 20, _restartButtonBounds.X + 10, _restartButtonBounds.Y + 10);

            SplashKit.FillRectangle(Color.Gray, _exitButtonBounds);
            SplashKit.DrawText("Exit", Color.White, "Arial", 20, _exitButtonBounds.X + 10, _exitButtonBounds.Y + 10);
        }
    }
}

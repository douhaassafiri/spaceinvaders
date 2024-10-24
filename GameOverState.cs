using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class GameOverState : IGameState
    {
        // Fields
        private GameUI _gameUI;
        private int _finalScore;

        // Constructors
        public GameOverState(GameUI gameUI, int score)
        {
            _gameUI = gameUI;
            _finalScore = score;
            Console.WriteLine("GAME OVER! Final score: " + _finalScore);
        }

        // Methods
        public void Handle(GameManager context)
        {
            Render();

            if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MousePosition(), _gameUI.RestartButtonBounds))
            {
                context.RestartGame();
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MousePosition(), _gameUI.ExitButtonBounds))
            {
                Console.WriteLine("Exiting game...");
                SplashKit.CloseWindow(_gameUI.GameWindow);
            }
        }

        private void Render()
        {
            SplashKit.ClearScreen(Color.Black);
            _gameUI.DrawGameOverScreen(_finalScore);
            SplashKit.RefreshScreen();
        }
    }
}


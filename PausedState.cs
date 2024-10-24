using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceinvaders
{
    public class PausedState : IGameState
    {
        // Fields
        private GameUI _gameUI;

        // Constructors
        public PausedState(GameUI gameUI)
        {
            _gameUI = gameUI;
        }

        // Methods
        public void Handle(GameManager context)
        {
            SplashKit.ClearScreen(Color.Black);
            _gameUI.DrawPauseMenu();

            if (SplashKit.MouseClicked(MouseButton.LeftButton) &&
                SplashKit.PointInRectangle(SplashKit.MousePosition(), _gameUI.MenuButtonBounds))
            {
                context.TogglePause();
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton) &&
                SplashKit.PointInRectangle(SplashKit.MousePosition(), _gameUI.RestartButtonBounds))
            {
                context.RestartGame();
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton) &&
                SplashKit.PointInRectangle(SplashKit.MousePosition(), _gameUI.ExitButtonBounds))
            {
                Console.WriteLine("Exiting game...");
                SplashKit.CloseWindow(_gameUI.GameWindow);
            }

            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                context.TogglePause();
            }
        }
    }
}

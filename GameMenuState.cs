using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class GameMenuState : IGameState
    {
        // Fields
        private GameUI _gameUI;

        // Constructor
        public GameMenuState(GameUI gameUI)
        {
            _gameUI = gameUI;
        }

        // Methods
        public void Handle(GameManager context)
        {
            SplashKit.ClearScreen(Color.Black);
            _gameUI.DrawMenu();

            if (SplashKit.KeyTyped(KeyCode.ReturnKey))
            {
                context.SetState(context.GetPlayingState());
            }
        }
    }
}

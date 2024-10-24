using System;
using SplashKitSDK;

namespace spaceinvaders
{
    public class Program
    {
        public static void Main()
        {
            GameManager game = new GameManager();
            game.GameLoop();
        }
    }
}

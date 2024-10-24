using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceinvaders
{
    public class Score : IObserver
    {
        // Fields
        private int _score;

        // Properties
        public int Value
        {
            get { return _score; }
        }

        // Constructors
        public Score()
        {
            _score = 0;
        }

        public void Update(int points)
        {
            _score += points;
        }

        public void Reset()
        {
            _score = 0;
        }

        public void DisplayScore()
        {
            SplashKit.DrawText("Score: " + _score, Color.White, "Arial", 20, 10, 10);
        }
    }
}


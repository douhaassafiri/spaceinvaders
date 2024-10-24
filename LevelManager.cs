using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class LevelManager
    {
        // Fields
        private const int _maxLevel = 5;
        private int _currentLevel;

        // Properties
        public int Level
        {
            get { return _currentLevel; }
        }

        public bool IsMaxLevel
        {
            get { return _currentLevel == _maxLevel; }
        }

        // Constructors
        public LevelManager()
        {
            _currentLevel = 1;
        }

        // Methods
        public void AdvanceLevel()
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
            }
        }

        public bool IsBossLevel()
        {
            return _currentLevel == _maxLevel;
        }

        public bool HasAlienChasers()
        {
            return _currentLevel == 3 || _currentLevel == 4; 
        }

        public int GetAlienChaserCount()
        {
            if (_currentLevel == 3) return 1;
            if (_currentLevel == 4) return 3;
            return 0;
        }

        public (int rows, int cols) GetEnemyFormationSize()
        {
            int rows = 0 + _currentLevel;
            int cols = 2 + _currentLevel;
            return (rows, cols);
        }

        public void ResetLevel()
        {
            _currentLevel = 1;
        }
    }
}

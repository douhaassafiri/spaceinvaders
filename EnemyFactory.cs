using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class EnemyFactory
    {
        // Constructors
        public EnemyFactory()
        {
        }

        // Methods
        public EnemyShip CreateEnemy(string type, float x, float y)
        {
            switch (type)
            {
                case "basic":
                    return new EnemyShip(x, y, "basic", this);
                case "chaser":
                    return new AlienChaser(x, y, this);
                case "boss":
                    return new BossAlien(x, y, this);
                default:
                    return new EnemyShip(x, y, "basic", this);
            }
        }
    }
}

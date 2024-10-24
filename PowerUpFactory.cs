using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public class PowerUpFactory
    {
        // Constructors
        public PowerUpFactory()
        {
        }

        // Methods
        public PowerUp CreatePowerUp(string type, float x, float y)
        {
            return new PowerUp(type, x, y);
        }
    }
}

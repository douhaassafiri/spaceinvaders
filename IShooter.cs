using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace spaceinvaders
{
    public interface IShooter
    {
        void Shoot(List<Projectile> projectiles);
    }
}

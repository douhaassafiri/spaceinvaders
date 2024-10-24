using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SplashKitSDK;

namespace spaceinvaders
{
    public class ScoreManager : ISubject
    {
        // Fields
        private List<IObserver> _observers;

        // Constructors
        public ScoreManager()
        {
            _observers = new List<IObserver>();
        }

        // Methods
        public void AddObserver(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void NotifyObservers(int points)
        {
            foreach (var observer in _observers)
            {
                observer.Update(points);
            }
        }

        public void EnemyDefeated(string enemyType)
        {
            int points = enemyType switch
            {
                "normal" => 100,
                "chaser" => 200,
                "boss" => 500,
                _ => 0
            };

            NotifyObservers(points);
        }

        public void PowerUpCollected()
        {
            NotifyObservers(100);
        }
    }
}

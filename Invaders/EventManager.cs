using System;

namespace Invaders
{
    public delegate void ValueChangedEvent(Scene scene, int amount);
    public class EventManager
    {
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent OnGainScore;
        private int healthLost;
        private int scoreGained;
        public void PublishLoseHealth(int amount) => healthLost += amount;
        public void PublicScore(int amount) => scoreGained += amount;
        
        public void HandelEvents(Scene scene)
        {
            if(healthLost != 0)
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }
            
            if (scoreGained != 0)
            {
                OnGainScore?.Invoke(scene, scoreGained);
                scoreGained = 0;
            }
        }

    }
}

using System;

namespace Invaders
{
    public delegate void ValueChangedEvent(Scene scene, int amount);
    public class EventManager
    {
        public event ValueChangedEvent LoseHealth;

        private int healthLost;

        public void PublishLoseHealth(int amount) => healthLost += amount;
        
        public void HandelEvents(Scene scene)
        {
            if(healthLost != 0)
            {
                LoseHealth?.Invoke(scene, healthLost);
                healthLost = 0;
            }
        }

    }
}

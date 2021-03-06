using System;

namespace Invaders
{
    public class Schedule : Entity
    {
        public event Action<Scene> Action;
        private float timeLeft;
        public Schedule(float time) : base ("")
        {
            timeLeft = time;
        }

        public override void Create(Scene scene)
        {
            scene.Update += Update;
        }
        
        protected override void Update(Scene scene, float deltaTime)
        {
            if((timeLeft -=deltaTime) > 0 && !Dead) return;
            Action?.Invoke(scene);
            timeLeft += 1;
        }
    }
}

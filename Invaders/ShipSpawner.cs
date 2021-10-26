using System;
using SFML.System;

namespace Invaders
{
    public class ShipSpawner : Entity
    {

        private float timeLeft;
        private float frequency;
        private Random generator;
        private float TimePlayed;

        private int score;
        public ShipSpawner() : base("")
        {
            generator = new Random();
        }

        public override void Create(Scene scene)
        {
            scene.Update += Update;
            frequency = generator.Next(1, 10)/ 10f;
            timeLeft = 2;
        }

        protected override void Update(Scene scene, float deltaTime)
        {
            

            if ((TimePlayed += deltaTime) > score + 1)
            {
                score++;
                scene.Events.PublicScore(1);
            }
            if ((timeLeft -= deltaTime) > 0) return;
            frequency = GetFrequency(TimePlayed);
            timeLeft = frequency;
            scene.Spawn(new EnemyShip{
                Position =  new Vector2f(generator.Next(50, Program.WindowW - 50), -50),
                direction = new Vector2f(generator.Next(0, 2) == 1 ? 1 : -1, (generator.Next(0, 11)/10f)) /MathF.Sqrt(2.0f)
                                    
                });

           
        }

        private float GetFrequency(float timePassed)
        {
            float result;

            result = -(MathF.Log(timePassed + 0.0015f) - 4);
            return result > 0.2f ? result : 0.2f;
        }

    }
}

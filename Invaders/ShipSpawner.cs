using System;
using SFML.System;

namespace Invaders
{
    public class ShipSpawner : Entity
    {

        private float timeLeft;
        private float frequenzy;
        private Random generator;
        public ShipSpawner() : base("")
        {
            generator = new Random();
        }

        public override void Create(Scene scene)
        {
            scene.Update += Update;
            frequenzy = generator.Next(1, 10)/ 10f;
            timeLeft = frequenzy;
        }
    
        public override void Update(Scene scene, float deltaTime)
        {
            if ((timeLeft -= deltaTime) > 0) return;
            frequenzy = generator.Next(5, 15)/ 10f;
            timeLeft = frequenzy;
            scene.Spawn(new EnemyShip{
                Position =  new Vector2f(generator.Next(50, Program.WindowW - 50), -50),
                direction = new Vector2f(generator.Next(0, 2) == 1 ? 1 : -1, (generator.Next(0, 11)/10f)) /MathF.Sqrt(2.0f)
                                    
                });

           
        }

    }
}

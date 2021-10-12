using System;
using SFML.System;

namespace Invaders
{
    public class ShipSpawner : Entity
    {

        private float timeLeft;
        private float frequenzy;
        public ShipSpawner() : base("")
        {
            
        }

        public override void Create(Scene scene)
        {
            scene.Update += Update;
            frequenzy = new Random().Next(1, 10)/ 10f;
            timeLeft = frequenzy;
        }
    
        public override void Update(Scene scene, float deltaTime)
        {
            if ((timeLeft -= deltaTime) > 0) return;
            frequenzy = new Random().Next(5, 30)/ 1f;
            timeLeft = frequenzy;
            scene.Spawn(new EnemyShip{
                Position =  new Vector2f(new Random().Next(50, Program.WindowW - 50), -50),
                direction = new Vector2f(new Random().Next(0, 2) == 1 ? 1 : -1, (new Random().Next(3, 11)/10f)) /MathF.Sqrt(2.0f)
                                    
                });

           
        }

    }
}

using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Explosion : Entity
    {
        private float time = 0;
        private float frameRate = 24;
        public Explosion() : base("explosion")
        {

        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(0, 0, 64 , 64);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            sprite.Scale = new Vector2f(1.5f, 1.5f);
            sprite.Rotation = new Random().Next(360);

            scene.Update += Update;
            scene.Render += Render;
        }
        private int row = 0;
        private int col = 0;
        public override void Update(Scene scene, float deltaTime)
        {

            if ((time += deltaTime) > 1/frameRate)
            {
                col++;
                if (col > 3)
                {
                    row++;
                    col = 0;
                }
                
                time = 0;
                sprite.TextureRect = new IntRect(col * 64 , row * 64, 64 , 64);
            }
            if (row > 3)
            {
                Dead = true;
            }
        }

        
    }
}

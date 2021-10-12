using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Bullet : Entity
    {
        private Vector2f direction;
        private float speed = 600f;

        public readonly Entity shotFrom;
        public Bullet(Vector2f direction, Entity shotFrom) : base("sheet")
        {
            this.direction = direction;
            this.shotFrom = shotFrom;
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = scene.Assets.LoadTile("laserBlue01");
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            sprite.Rotation = ((180 / MathF.PI) * MathF.Atan2(direction.Y, direction.X)) + 90;
            scene.Update += Update;
            scene.Render += Render;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            
            Position += direction * speed * deltaTime;

            if (Position.X < 0 - sprite.Origin.X||
                Position.X > Program.WindowW + sprite.Origin.X||
                Position.Y < 0 - sprite.Origin.Y||
                Position.Y > Program.WindowH + sprite.Origin.Y)
            {
                Dead = true;
            }
            
        }

        public override void Render(RenderTarget target)
        {
            base.Render(target);

        }

        

    }
}

using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class EnemyShip : Entity
    {

        public Vector2f direction = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
        public EnemyShip() : base("sheet")
        {

        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = scene.Assets.LoadTile("enemyBlack");
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            sprite.Rotation = ((180 / MathF.PI) * MathF.Atan2(direction.Y, direction.X)) + -90;
            Schedule schedule =  new Schedule(1);
            schedule.Action += (Scene Scene) => Scene.Spawn(new Bullet(direction){Position = (this.Position + (direction * 120)) });
            scene.Spawn(schedule);
            
            scene.Update += Update;
            scene.Render += Render;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            var newPos = Position;
            newPos += direction * deltaTime * 300.0f;
            
            if (newPos.X > Program.WindowW - sprite.Origin.X)
            {
                newPos.X = Program.WindowW - sprite.Origin.X;
                Reflect(new Vector2f(-1,0));
            }
            else if (newPos.Y > Program.WindowH - sprite.Origin.X)
            {
                newPos.Y = Program.WindowH - sprite.Origin.X;
                Reflect(new Vector2f(0,-1));

            }
            else if (newPos.X < 0 + sprite.Origin.X)
            {
                newPos.X = 0 + sprite.Origin.X;
                Reflect(new Vector2f(1,0));
            }
            else if (newPos.Y < 0 + sprite.Origin.X)
            {
                newPos.Y = 0 + sprite.Origin.X;
                Reflect(new Vector2f(0,1));
            }
            Position = newPos;
        }

        private void Reflect(Vector2f normal)
        {
            direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
            sprite.Rotation = ((180 / MathF.PI) * MathF.Atan2(direction.Y, direction.X)) + -90;
        }

        public override void Render(RenderTarget target)
        {
            
            
            target.Draw(sprite);
        }

    }
}

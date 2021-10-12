using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class EnemyShip : Entity
    {

        public Vector2f direction = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
        private Schedule schedule;
        private float speed = 300.0f;
        public EnemyShip() : base("sheet")
        {
            schedule = new Schedule(1);
            //TODO: Check if there is a better way to spawn bullets
            //
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = scene.Assets.LoadTile("enemyBlack");
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            sprite.Rotation = ((180 / MathF.PI) * MathF.Atan2(direction.Y, direction.X)) + -90;
            
            schedule.Action += (Scene Scene) => Scene.Spawn(new Bullet(direction, this){Position = (this.Position + (direction * 120)) });
            scene.Spawn(schedule);
            
            scene.Update += Update;
            scene.Render += Render;
        }

        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            schedule.Dead = true;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            
            Vector2f newPos = Position;
            newPos += direction * deltaTime * speed;
            
            if (newPos.X > Program.WindowW - sprite.Origin.X)
            {
                newPos.X = Program.WindowW - sprite.Origin.X;
                Reflect(new Vector2f(-1,0));
            }
            else if (newPos.Y > Program.WindowH - sprite.Origin.X)
            {
                newPos = new Vector2f(Position.X , 0);
                System.Console.WriteLine();

            }
            else if (newPos.X < 0 + sprite.Origin.X)
            {
                newPos.X = 0 + sprite.Origin.X;
                Reflect(new Vector2f(1,0));
            }
            Position = newPos;
            base.Update(scene, deltaTime);
        }
        
        protected override void CollideWith(Scene scene, Entity other)
        {
            if(other is Bullet bullet)
            {
                if(!(bullet.shotFrom is EnemyShip))
                {
                    if (bullet.shotFrom != this)
                    {
                        other.Dead = true;
                        Dead = true;
                    }
                }
                
            }
            
            
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

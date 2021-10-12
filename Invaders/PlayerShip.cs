using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class PlayerShip : Entity
    {
        private float speed = 500;
        private float timer = 0;
        private float attackSpeed = 0.5f;

        public PlayerShip() : base("sheet")
        {

        }
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = scene.Assets.LoadTile("playerShip1_blue");
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            scene.Update += Update;
            scene.Render += Render;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            timer += deltaTime;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Right)) Position += new Vector2f(1,0) * speed * deltaTime;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Down)) Position += new Vector2f(0, 1) * speed * deltaTime;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Left)) Position += new Vector2f(-1, 0) * speed * deltaTime;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Up)) Position += new Vector2f (0, -1) * speed * deltaTime;

            if(Keyboard.IsKeyPressed(Keyboard.Key.Space) && timer > attackSpeed)
            {
                scene.Spawn(new Bullet(new Vector2f(0, -1), this){Position = this.Position});
                timer = 0;
            }
        }

        protected override void CollideWith(Scene scene, Entity other)
        {
            if(other is Bullet bullet)
            {
                
                if (bullet.shotFrom != this)
                {
                    other.Dead = true;
                    Dead = true;
                }
                
            }
            
            
        }
    }
}

using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders
{
    public class PlayerShip : Entity
    {
        private float speed = 500;
        private float attackSpeedTimer = 0;
        private float attackSpeed = 0.2f;
        private float iFrameTimer = 0;

        public PlayerShip() : base("sheet")
        {
            
        }
        public override bool Solid => true;
        public override void Create(Scene scene)
        {
            base.Create(scene);
            Position = new Vector2f(Program.WindowW/2 , Program.WindowW -100);
            sprite.TextureRect = scene.Assets.LoadTile("playerShip1_blue");
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            scene.Events.LoseHealth += LostHealth;
            scene.Update += Update;
            scene.Render += Render;
        }

        protected override void Update(Scene scene, float deltaTime)
        {
            
            attackSpeedTimer += deltaTime;
            if ((iFrameTimer -= deltaTime) <= 0) 
            {
                iFrameTimer = 0;
                sprite.Color = new Color(255, 255, 255);
            }
            
            
            if(Keyboard.IsKeyPressed(Keyboard.Key.Right)) Position += new Vector2f(1,0) * speed * deltaTime;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Down)) Position += new Vector2f(0, 1) * speed * deltaTime;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Left)) Position += new Vector2f(-1, 0) * speed * deltaTime;
            if(Keyboard.IsKeyPressed(Keyboard.Key.Up)) Position += new Vector2f (0, -1) * speed * deltaTime;

            if (Position.X < 0 + sprite.Origin.X)
            {
                Position = new Vector2f(0 + sprite.Origin.X, Position.Y);
            }
            if (Position.X > Program.WindowW - sprite.Origin.X)
            {
                Position = new Vector2f(Program.WindowW - sprite.Origin.X, Position.Y);
            }
            if (Position.Y < 0 + sprite.Origin.Y)
            {
                Position = new Vector2f(Position.X, 0 + sprite.Origin.Y);
            }
            if (Position.Y > Program.WindowH - sprite.Origin.Y)
            {
                Position = new Vector2f(Position.X, Program.WindowH - sprite.Origin.Y);
            }

            if(Keyboard.IsKeyPressed(Keyboard.Key.Space) && attackSpeedTimer > attackSpeed)
            {
                scene.Spawn(new Bullet(new Vector2f(0, -1), this){Position = this.Position - new Vector2f(0, this.sprite.Origin.Y)});
                attackSpeedTimer = 0;
            }
            base.Update(scene, deltaTime);
        }

        protected override void CollideWith(Scene scene, Entity other)
        {
            if(other is Bullet bullet)
            {
                if (bullet.shotFrom != this)
                {
                    other.Dead = true;
                    if (iFrameTimer <= 0)
                    {
                        scene.Events.PublishLoseHealth(1);
                    }
                } 
            }
            else if (other is EnemyShip enemyShip)
            {
                if (iFrameTimer <= 0)
                {
                    enemyShip.Dead = true;
                    scene.Events.PublishLoseHealth(1);
                }
            }
        }

        private void LostHealth(Scene scene, int amount)
        {
            iFrameTimer = 2;
            sprite.Color =  new Color(128, 128 , 128);
            //Sprite dmgSprite = new Sprite(scene.Assets.LoadTexture("sheet"));
            //dmgSprite.TextureRect = scene.Assets.LoadTile("playerShip1_damage2");
            //sprite.Color += dmgSprite.Color;
        }
    }
}

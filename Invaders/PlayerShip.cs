using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class Player : Entity
    {
        public Player() : base("sheet")
        {

        }
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(425, 384, 93, 84);
            sprite.Origin = new Vector2f(sprite.TextureRect.Width/2 , sprite.TextureRect.Height/2);
            scene.Update += Update;
            scene.Render += Render;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            
        }
    }
}

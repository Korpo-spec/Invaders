using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class EnemyShip : Entity
    {

        public EnemyShip() : base("spaceSheet")
        {

        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(143, 377, 43, 31);
            scene.Update += Update;
            scene.Render += Render;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }

    }
}

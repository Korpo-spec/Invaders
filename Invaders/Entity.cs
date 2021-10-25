using System;
using SFML.Graphics;
using SFML.System;
using System.Linq;

namespace Invaders
{
    public class Entity
    {
        private string textureName = "";
        protected Sprite sprite;
        public bool Dead;

        public Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }
        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }

        public virtual bool Solid => false;

        public virtual FloatRect Bounds => sprite.GetGlobalBounds();

        public virtual void Create(Scene scene)
        {

            sprite.Texture = scene.Assets.LoadTexture(textureName);
            
        }

        public virtual void Destroy(Scene scene)
        {
            scene.Update -= Update;
            scene.Render -= Render;
        }
        public virtual void Update(Scene scene, float deltaTime)
        {
            
            foreach (Entity found in scene.FindIntersects(Bounds).Where(e => e.Solid)) //Find collisions
            {
                CollideWith(scene, found);
            }
            
        }

        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }

        protected virtual void CollideWith(Scene scene, Entity other)
        {
            
        }

        
    }
}

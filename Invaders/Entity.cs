using System;
using SFML.Graphics;
using SFML.System;

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

        public virtual void Create(Scene scene)
        {
            //sprite.Texture = scene.Assets.LoadTexture(textureName);
            
        }

        public virtual void Destroy(Scene scene)
        {
            
        }

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
    }
}

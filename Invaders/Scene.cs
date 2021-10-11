using System;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Invaders
{
    public delegate void UpdateEvent(Scene scene, float deltaTime);
    public delegate void RenderEvent(RenderTarget target);
    public class Scene
    {
        private readonly List<Entity> entities = new List<Entity>();
        public event UpdateEvent Update;
        public event RenderEvent Render;

        public readonly AssetManager Assets;
        public Scene()
        {
            Assets = new AssetManager();
        }

        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public void UpdateAll(float deltaTime)
        {
            Update?.Invoke(this, deltaTime);
        }

        public void RenderAll(RenderTarget target)
        {
            Render?.Invoke(target);
        }


    }
}

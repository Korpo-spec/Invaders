using System;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;

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

            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                
                if(entity.Dead)
                {
                    entity.Destroy(this);
                    entities.RemoveAt(i);
                }
                else
                    i++;
            }
            
        }

        public void RenderAll(RenderTarget target)
        {
            Render?.Invoke(target);
        }

        public IEnumerable<Entity> FindIntersects(FloatRect bounds)//Find all intersects between chosen bounds and all other entities
        {
            int lastEntity = entities.Count - 1;

            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (entity.Dead) continue;
                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }
            }
        }


    }
}

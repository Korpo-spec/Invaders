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
        public event RenderEvent GuiRender;

        public readonly AssetManager Assets;
        public readonly EventManager Events;
        public readonly SceneLoader sceneLoader;
        public Scene()
        {
            Assets = new AssetManager();
            Events =  new EventManager();
            sceneLoader =  new SceneLoader();
        }

        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public void Clear() //Clear the whole entities list and play entities destroy function
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
                
            }
        }

        public void UpdateAll(float deltaTime)
        {
            sceneLoader.HandleSceneLoad(this);

            Update?.Invoke(this, deltaTime);

            Events.HandelEvents(this);

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
            GuiRender?.Invoke(target);
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

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
        public event EventHandler<UpdateEvent> Update;
        public event EventHandler<RenderEvent> Render;


    }
}

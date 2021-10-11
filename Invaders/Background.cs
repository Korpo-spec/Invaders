using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace Invaders
{
    public class Background : Entity
    {
        public Background() : base("darkPurple")
        {

        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            scene.Render += Render;
        }
        

        public override void Render(RenderTarget target)
        {
            View view = target.GetView();
            Vector2f topLeft = view.Center - 0.5f * view.Size;

            int tilesX = (int) MathF.Ceiling(view.Size.X / sprite.TextureRect.Width);
            int tilesY = (int) MathF.Ceiling(view.Size.Y / sprite.TextureRect.Height);

            for (int y = 0; y < tilesY; y++)
            {
                for (int x = 0; x < tilesX; x++)
                {
                    sprite.Position = topLeft +  new Vector2f(x * sprite.TextureRect.Width , y * sprite.TextureRect.Height);
                    base.Render(target);
                }
            }
            

            
        }
    }
}

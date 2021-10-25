using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class GUI : Entity
    {
        private Text gui;
        private int score;
        private int hp = 3;
        public GUI() : base("sheet")
        {
            
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            gui = new Text();
            scene.Events.OnGainScore += scoreGain;
            gui.DisplayedString = $"Score: {score}";
            gui.Position = new Vector2f(50, 50);
            gui.CharacterSize = 24;
            gui.Font = scene.Assets.LoadFont("kenvector_future");
            scene.GuiRender += Render;
            sprite.TextureRect = scene.Assets.LoadTile("playerLife1_blue");
            sprite.Scale = new Vector2f(2, 2);
            scene.Events.LoseHealth += LostHealth;
        }

        public override void Destroy(Scene scene)
        {
            scene.GuiRender -= Render;
        }

        private void scoreGain(Scene scene, int amount){
            score += amount;
            
        }

        private void LostHealth(Scene scene, int amount)
        {
            if ((hp -= amount) <= 0)
            {
                scene.sceneLoader.Reload();
            }
            
        }

        public override void Render(RenderTarget target)
        {
            gui.DisplayedString = $"Score: {score}";
            target.Draw(gui);
            Position = new Vector2f(Program.WindowW - 150 , 50);
            
            for (int i = 0; i < hp; i++)
            {
                target.Draw(sprite);
                Position += new Vector2f(25, 0);
            }
        }
    }
}

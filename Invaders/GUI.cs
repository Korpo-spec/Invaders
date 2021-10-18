using System;
using SFML.Graphics;
using SFML.System;

namespace Invaders
{
    public class GUI : Entity
    {
        private Text gui;
        private int score;
        public GUI() : base("")
        {
            
        }

        public override void Create(Scene scene)
        {
            gui = new Text();
            scene.Events.OnGainScore += scoreGain;
            gui.DisplayedString = $"Score: {score}";
            gui.Position = new Vector2f(50, 50);
            gui.CharacterSize = 24;
            gui.Font = scene.Assets.LoadFont("kenvector_future");
            scene.Render += Render;

        }

        private void scoreGain(Scene scene, int amount){
            score += amount;
            
        }

        public override void Render(RenderTarget target)
        {
            gui.DisplayedString = $"Score: {score}";
            target.Draw(gui);
        }
    }
}

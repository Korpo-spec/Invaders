﻿using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Invaders
{
    class Program
    {
        public const int WindowW = 828;
        public const int WindowH = 900;
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(WindowW, WindowH), "Invaders")) 
            {
                window.Closed += (o, e) => window.Close();
                
                
                Scene scene = new Scene();
                scene.Spawn(new Background());
                scene.Spawn(new EnemyShip(){Position =  new Vector2f(50, 50)});
                scene.Spawn(new PlayerShip());
                Clock clock = new Clock();
                
                while (window.IsOpen)
                {
                    
                    window.DispatchEvents();

                    float deltaTime = clock.Restart().AsSeconds();
                    if (deltaTime > 0.01f) deltaTime = 0.01f;
                    
                    //TODO: UpdateAll
                    scene.UpdateAll(deltaTime);
                    
                    window.Clear(new Color(223, 246, 245));
                    // TODO:  Drawing
                    scene.RenderAll(window);
                    
                    window.Display();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SFML.System;

namespace Invaders
{
    public class SceneLoader
    {

        private readonly Dictionary<char, Func<Entity>> loaders;

        private string currentScene = "", nextScene = "";

        public SceneLoader()
        {
            loaders = new Dictionary<char, Func<Entity>>()
            {
                {'p', () => new PlayerShip()},
                {'e', () => new EnemyShip()},
                {'w', () => new ShipSpawner()},
                {'g', () => new GUI()}
            };
        }

        private bool Create(char symbol, out Entity created)
        {
            if (loaders.TryGetValue(symbol, out Func<Entity> loader))
            {
                created = loader();
                
                return true;
            }

            created = null;
            return false;
        }

        public void HandleSceneLoad(Scene scene)
        {
            if (nextScene == "") return;
            scene.Clear();
            string file = $"assets/{nextScene}.txt";
            string[] board = File.ReadAllLines(file, Encoding.UTF8);

            scene.Spawn(new Background());
            for (int i = 0; i < board.Length; i++) 
            {
                if (Create(board[i][0], out Entity entity))
                {
                    string[] attributes =  board[i].Split(" ");
                    entity.Position = new Vector2f(float.Parse(attributes[1]), float.Parse(attributes[2]));
                    scene.Spawn(entity);
                }

                
            }
            
            currentScene = nextScene;
            nextScene = "";
            
            
        }
        public void Load(string scene) => nextScene = scene;
        public void Reload() => nextScene = currentScene;

    }
}

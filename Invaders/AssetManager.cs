using System;
using SFML.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Invaders
{
    public class AssetManager
    {
        public static readonly string AssetPath = "assets";
        private readonly Dictionary<string, Texture> textures;

        private readonly Dictionary<string, IntRect> tiles;
        private readonly Dictionary<string, Font> fonts;

        public AssetManager()
        {
            textures = new Dictionary<string, Texture>();
            fonts = new Dictionary<string, Font>();
            tiles =  new Dictionary<string, IntRect>();
        }

        public Texture LoadTexture(string name)
        {
            if (textures.TryGetValue(name,out Texture found))
            {
                return found;
            }
            string fileName = $"assets/{name}.png";
            Texture texture = new Texture(fileName);
            textures.Add(name, texture);
            return texture;
        }

        public IntRect LoadTile(string name)
        {
            if(tiles.TryGetValue(name, out IntRect found))
            {
                return found;
            }
            string fileName = $"assets/sheet.xml";
            int[] numbers =  new int[4];
            foreach (string line in File.ReadLines(fileName, Encoding.UTF8))
            {
                
                if(line.Contains(name))
                {
                    string[] contains = line.Split(" ");
                    
                    for (int i = 1; i < contains.Length; i++)
                    {
                        string[] kok = contains[i].Split('"');
                        numbers[i-1] = int.Parse(kok[1]);
                    }
                    return new IntRect(numbers[0], numbers[1], numbers[2],numbers[3]);
                }
            }
            
            return new IntRect(numbers[0], numbers[1], numbers[2],numbers[3]);
        }

        public Font LoadFont(string name)
        {
            if (fonts.TryGetValue(name,out Font found))
            {
                return found;
            }
            string fileName = $"assets/{name}.ttf";
            Font font = new Font(fileName);
            fonts.Add(name, font);
            return font;
        }
    }
}

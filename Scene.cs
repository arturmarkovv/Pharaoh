using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    public class Scene
    {
        private List<GameObject> _gameObjects { get;}

        public Scene()
        {
            _gameObjects = new List<GameObject>();
        }

        public List<GameObject> GameObjects => _gameObjects;
        public void AddGameObject(GameObject gameObj)
        {
            _gameObjects.Add(gameObj);
        }

        public void AddGameObject(List<GameObject> gameObjects)
        {
            _gameObjects.AddRange(gameObjects);
        }
    }


    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class GameObject
    {
        public string Name { get; set; }
        public string Value{ get; set; }
        public Position Position{ get; set; }
        public ConsoleColor? BgColor { get; set; }
        public ConsoleColor? FgColor { get; set; }

        public GameObject(string name, string value, Position position, ConsoleColor? bgColor, ConsoleColor? fgColor, bool offset = true)
        {
            Name = name;
            Value = value;
            Position = position;
            BgColor = bgColor;
            FgColor = fgColor;
            if(offset)Offset();
        }

        private void Offset()
        {
            if (Position.X - Value.Length / 2 >= 0)
            {
                Position.X -= Value.Length / 2;
            }
        }
    }
}

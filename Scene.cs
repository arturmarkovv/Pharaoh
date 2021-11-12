using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    public class Scene
    {
        public List<Content> GameObjects { get; set; }

        public Scene()
        {
            GameObjects = new List<Content>();
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

    public class Content
    {
        public string Name { get; set; }
        public string Value{ get; set; }
        public Position Position{ get; set; }
        public ConsoleColor? BGColor { get; set; }
        public ConsoleColor? FGColor { get; set; }

        public Content(string name, string value, Position position, ConsoleColor? bgColor, ConsoleColor? fgColor, bool offset = true)
        {
            Name = name;
            Value = value;
            Position = position;
            BGColor = bgColor;
            FGColor = fgColor;
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

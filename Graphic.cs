using System;
using System.Text;
using Pharaoh.Helpers;

namespace Pharaoh
{
    public static class Graphic
    {
        public static void WindowSetup()
        {
            Console.OutputEncoding = Encoding.UTF8; //расширяем допустимую консоли кодировку
            ConsoleHelper.SetCurrentFont("Consolas", 32);//устанавливаем оптимальный шрифт
            Console.SetWindowPosition(0,0);
        }

        public static void Update(Scene scene)
        {
            Console.Clear();
            foreach (var gameObj in scene.GameObjects)
            {
                Console.SetCursorPosition(gameObj.Position.X, gameObj.Position.Y);
                if (gameObj.BGColor != null) Console.BackgroundColor = (ConsoleColor) gameObj.BGColor;
                if (gameObj.FGColor != null) Console.ForegroundColor = (ConsoleColor) gameObj.FGColor;
                Console.Write(gameObj.Value);
                Console.ResetColor();
            }
        }
    }
}
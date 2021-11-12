using Pharaoh.Helpers;
using System;
using System.Text;
using System.Threading;

namespace Pharaoh
{
    public static class Graphic
    {
        public static void WindowSetup()
        {
            Console.OutputEncoding = Encoding.UTF8; //расширяем допустимую консоли кодировку
            ConsoleHelper.SetCurrentFont("Consolas", 32);//устанавливаем оптимальный шрифт
            ConsoleHelper.ShowMaximizeConsole();
        }

        public static void Update(Scene scene)
        {
            Console.Clear();
            Console.CursorVisible = false;

            foreach (var gameObj in scene.GameObjects)
            {
                Console.SetCursorPosition(
                    ValidatePosX(gameObj.Position.X, gameObj.Value.Length),
                    ValidatePosY(gameObj.Position.Y));

                if (gameObj.BgColor != null)
                {
                    Console.BackgroundColor = (ConsoleColor)gameObj.BgColor;
                }

                if (gameObj.FgColor != null)
                {
                    Console.ForegroundColor = (ConsoleColor)gameObj.FgColor;
                }

                Console.Write(gameObj.Value);

                Console.ResetColor();
            }
            Thread.Sleep(500);

        }

        private static int ValidatePosX(int x, int valueLength)
        {
            if (x < 0)
            {
                x = 0;
            }
            else if (x + valueLength > Console.WindowWidth)
            {
                x = Console.WindowWidth - valueLength;
            }

            return x;
        }
        private static int ValidatePosY(int y)
        {
            if (y < 0)
            {
                y = 0;
            }
            else if (y > Console.WindowHeight)
            {
                y = Console.WindowHeight;
            }

            return y;
        }
    }
}
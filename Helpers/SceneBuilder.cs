using Pharaoh.GameInstance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharaoh.Helpers
{
    public static class SceneBuilder
    {
        private static readonly int Bottom = Console.WindowHeight - 1;
        private static readonly int Top = 0;
        private static readonly int Left = 0;
        private static readonly int Right = Console.WindowWidth - 1;
        private static readonly int WCenter = Right / 2;
        private static readonly int HCenter = Bottom / 2;


        public static void SetCursorToInputPos() => Console.SetCursorPosition(0, Console.WindowHeight - 2);
        public static void DrawScreen(Game game, SceneType sceneType)
        {
            var scene = new Scene();
            AddTitle(scene);

            switch (sceneType)
            {
                case SceneType.Start:
                    StartScene(scene);
                    break;
                case SceneType.Draw:
                    break;
                case SceneType.Game:
                    break;
                case SceneType.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null);
            }

            Graphic.Update(scene);
        }

        private static void AddTitle(Scene scene)
        {
            scene.GameObjects.Add(new Content("Title", "Welcome to Pharaoh!", new Position(Left, Top), ConsoleColor.Blue,
                ConsoleColor.DarkBlue));
        }

        private static void GenerateArt(Scene scene)
        {
            string[] art = {
                @" _____                      ",
                @"|A .  | _____               ",
                @"| /.\ ||A ^  | _____        ",
                @"|(_._)|| / \ ||A _  | _____ ",
                @"|  |  || \ / || ( ) ||A_ _ |",
                @"|____V||  .  ||(_ _)||( v )|",
                @"       |____V||  |  || \ / |",
                @"              |____V||  .  |",
                @"                     |____V|"
            };
            var index = 0;
            var contents = art
                .Select(line =>
                    new Content("Art", line, new(WCenter, (HCenter - art.Length/2) + index++), null, null))
                .ToList();
            scene.GameObjects.AddRange(contents);
        }
        private static void StartScene(Scene scene)
        {
            GenerateArt(scene);
            scene.GameObjects.AddRange(new List<Content>()
                {
                    new("Massage","Enter you name here: ",new Position(WCenter,Top+1),null,ConsoleColor.DarkBlue),
                    new("Controls","Press Enter to accept.",new Position(Left,Bottom-2),ConsoleColor.Blue,ConsoleColor.DarkBlue)
                }
            );
        }
        private static void DrawScene(Scene scene)
        {
            scene.GameObjects.AddRange(new List<Content>()
                {
                    new("Massage", "Enter you name here:", new Position(WCenter, Top + 1), null, ConsoleColor.DarkBlue),
                    new("Controls", "Press Enter to accept.", new Position(Left, Bottom - 2), ConsoleColor.Blue,
                        ConsoleColor.DarkBlue)
                }
            );
            //    public Content Massage { get; set; }
            //public Content Art { get; set; }
            //public Content EnemyTitle { get; set; }
            //public Content EnemyDice { get; set; }
            //public Content PlayerTitle { get; set; }
            //public Content PlayerDice { get; set; }
        }
        private static void GameScene(Scene scene)
        {
            //        public Content EnemyTitle { get; set; }
            //public Content EnemyHand { get; set; }
            //public Content PlayTable { get; set; }
            //public Content PlayerHand { get; set; }
            //public Content PlayerTitle { get; set; }
        }
        private static void EndScene(Scene scene)
        {
            //public Content Massage { get; set; }

        }

        public enum SceneType
        {
            Start,
            Draw,
            Game,
            End
        }
    }
}

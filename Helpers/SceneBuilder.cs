using Pharaoh.GameInstance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharaoh.Helpers
{
    static class SceneBuilder
    {
        private static readonly int Bottom = Console.WindowHeight - 1;
        private static readonly int Top = 0;
        private static readonly int Left = 0;
        private static readonly int Right = Console.WindowWidth - 1;
        private static readonly int WCenter = Right / 2;
        private static readonly int HCenter = Bottom / 2;

        private static int RelativeX(double mult)
        {
            mult = mult switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => mult
            };
            return Convert.ToInt32(Right * mult);
        }
        private static int RelativeY(double mult)
        {
            mult = mult switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => mult
            };
            return Convert.ToInt32(Bottom * mult);
        }


        public static void SetCursorToInputPos()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.CursorVisible = true;
        }
        
        public static Scene BuildScene(Game game, SceneType sceneType)
        {
            var scene = new Scene();
            AddTitle(scene);

            switch (sceneType)
            {
                case SceneType.Start:
                    StartScene(scene);
                    break;
                case SceneType.Drawing:
                    DrawScene(scene,game);
                    break;
                case SceneType.Game:
                    break;
                case SceneType.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null);
            }

            return scene;
        }

        private static void AddTitle(Scene scene)
        {
            scene.AddGameObject(new GameObject("Title", "Welcome to Pharaoh!", new Position(Left, Top), ConsoleColor.Blue,
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
                    new GameObject("Art", line, new(WCenter, (HCenter - art.Length/2) + index++), null, null))
                .ToList();
            scene.AddGameObject(contents);
        }
        private static void StartScene(Scene scene)
        {
            GenerateArt(scene);
            scene.AddGameObject(new List<GameObject>()
                {
                    new("Massage","Enter you name here: ",new Position(WCenter,Top+1),null,ConsoleColor.DarkBlue),
                    new("Controls","Press Enter to accept.",new Position(Left,Bottom-2),ConsoleColor.Blue,ConsoleColor.DarkBlue)
                }
            );
        }
        private static void DrawScene(Scene scene,Game game)
        {
            GenerateArt(scene);
            scene.AddGameObject(new List<GameObject>()
                {
                    new("Massage", "Who will go first?", new Position(WCenter, Top + 1), null, ConsoleColor.Green),
                    new("AI lable", $"AI: {game.EnemyDice}", new Position(RelativeX(0.425), RelativeY(0.75)), null, null),
                    new("Player lable", $"Player: {game.PlayerDice}", new Position(RelativeX(0.525), RelativeY(0.75)), null, null),
                    new("Controls", "Press Enter to accept.", new Position(Left, Bottom - 2), ConsoleColor.Blue,
                        ConsoleColor.DarkBlue),
                }
            );
            //    public GameObject Massage { get; set; }
            //public GameObject Art { get; set; }
            //public GameObject EnemyTitle { get; set; }
            //public GameObject EnemyDice { get; set; }
            //public GameObject PlayerTitle { get; set; }
            //public GameObject PlayerDice { get; set; }
        }
        private static void GameScene(Scene scene)
        {
            //        public GameObject EnemyTitle { get; set; }
            //public GameObject EnemyHand { get; set; }
            //public GameObject PlayTable { get; set; }
            //public GameObject PlayerHand { get; set; }
            //public GameObject PlayerTitle { get; set; }
        }
        private static void EndScene(Scene scene)
        {
            //public GameObject Massage { get; set; }

        }

        public enum SceneType
        {
            Start,
            Drawing,
            Game,
            End
        }
    }
}

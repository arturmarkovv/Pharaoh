using Pharaoh.GameInstance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pharaoh.Helpers
{
    internal static class SceneBuilder
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
            Console.SetCursorPosition(0, Bottom);
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
                    DrawScene(scene, game);
                    break;
                case SceneType.Game:
                    GameScene(scene, game);
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
                    new GameObject("Art", line, new(WCenter, (HCenter - art.Length / 2) + index++), null, null))
                .ToList();
            scene.AddGameObject(contents);
        }
        private static void StartScene(Scene scene)
        {
            GenerateArt(scene);
            scene.AddGameObject(new List<GameObject>()
                {
                    new("Massage","Enter you name here: ",new Position(WCenter,Top+1),null,ConsoleColor.DarkBlue),
                    new("Controls","Press Enter to accept.",new Position(Left,Bottom-1),ConsoleColor.Blue,ConsoleColor.DarkBlue)
                }
            );
        }
        private static void DrawScene(Scene scene, Game game)
        {
            GenerateArt(scene);
            scene.AddGameObject(new List<GameObject>()
                {
                    new("Massage", "Who will go first?", new Position(WCenter, Top + 1), null, ConsoleColor.Green),
                    new("AI lable", $"AI: {game.EnemyDice}", new Position(RelativeX(0.425), RelativeY(0.75)), null, null),
                    new("Player lable", $"Player: {game.PlayerDice}", new Position(RelativeX(0.525), RelativeY(0.75)), null, null),
                    new("Controls", "Press Enter to accept.", new Position(Left, Bottom - 1), ConsoleColor.Blue,
                        ConsoleColor.DarkBlue),
                }
            );
        }
        private static void GameScene(Scene scene, Game game)
        {
            var enemyCards = game.Enemy.HandCards.Aggregate(string.Empty, (current, card) => current + "▓ ");
            scene.AddGameObject(FillTableCards(game.TopCard, game.Deck.Count, new Position(WCenter, HCenter)));
            scene.AddGameObject(!game.IsSuitChangingTurn
                ? FillPlayerHand(game.Player.HandCards, new Position(WCenter, RelativeY(0.75)))
                : FillSuits(new Position(WCenter, RelativeY(0.75))));


            scene.AddGameObject(new List<GameObject>()
                {
                    new("AI Title", game.Enemy.Name, new Position(WCenter, RelativeY(0.23)), ConsoleColor.DarkMagenta, null),
                    new("AI Hand", enemyCards, new Position(RelativeX(0.5), RelativeY(0.25)), null, null),

                    new("Player Title", game.Player.Name, new Position(WCenter, RelativeY(0.77)),ConsoleColor.DarkMagenta,null),
                    new("Controls", "0:(1st)draw a card(2nd)skip turn; 1-9:play a card(choose suit); Enter:accept", new Position(Left, Bottom - 1), ConsoleColor.Blue,
                        ConsoleColor.DarkBlue),
                }
            );
        }
        private static void EndScene(Scene scene)
        {
            //public GameObject Massage { get; set; }

        }

        private static List<GameObject> FillSuits(Position pos)
        {
            var gameObjects = new List<GameObject>();
            var counter = 0;
            foreach (var suit in Enum.GetValues(typeof(Suit)))
            {
                gameObjects.Add(new GameObject("suit", GetSuitSymbol((Suit)suit) + "   ", new Position(pos.X - ((Enum.GetValues(typeof(Suit)).Length*3) - counter*3), pos.Y), null, GetCardColor((Suit)suit), false));
                counter++;
            }

            return gameObjects;
        }
        private static List<GameObject> FillTableCards(ICard topCard, int deckCount, Position pos, Suit? tempSuit = null)
        {
            var example = $"6♥  ▓{deckCount}";
            var gameObjects = new List<GameObject>();
            gameObjects.Add(new GameObject("Top card Quality", GetQualitySymbol(topCard.Quality), new Position(pos.X - example.Length / 2, pos.Y), null, null, false));
            gameObjects.Add(new GameObject("Top card Suit", GetSuitSymbol(topCard.Suit), new Position(pos.X - ((example.Length / 2) - 2), pos.Y), null, GetCardColor(topCard.Suit), false));
            gameObjects.Add(new GameObject("Deck", $"   ▓{deckCount}", new Position(pos.X - ((example.Length / 2) - 5), pos.Y), null, null, false));
            return gameObjects;
        }
        private static List<GameObject> FillPlayerHand(List<ICard> cards, Position pos)
        {
            var offset = cards.Count * 2;
            var gameObjects = new List<GameObject>();
            var counter = 0;
            foreach (var card in cards)
            {
                gameObjects.Add(new GameObject("Player card", GetQualitySymbol(card.Quality), new Position(pos.X - (offset - counter), pos.Y), null, null, false));
                counter += 2;
                gameObjects.Add(new GameObject("Player card", GetSuitSymbol(card.Suit), new Position(pos.X - (offset - counter), pos.Y), null, GetCardColor(card.Suit), false));
                counter += 3;
            }

            return gameObjects;
        }
        private static ConsoleColor GetCardColor(Suit suit)
        {
            return suit is Suit.Hearts or Suit.Diamonds ? ConsoleColor.Red : ConsoleColor.DarkCyan;
        }

        private static string GetSuitSymbol(Suit suit)
        {
            var suits = new[] { "♠", "♥", "♦", "♣" };
            return suit switch
            {
                Suit.Hearts => suits[1],
                Suit.Diamonds => suits[2],
                Suit.Spades => suits[0],
                Suit.Clubs => suits[3],
                _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
            };
        }
        private static string GetQualitySymbol(Quality quality)
        {
            var suits = new[] { "♠", "♥", "♦", "♣" };
            return quality switch
            {
                Quality.Ace => quality.ToString()[..1],
                Quality.King => quality.ToString()[..1],
                Quality.Queen => quality.ToString()[..1],
                Quality.Jack => quality.ToString()[..1],
                Quality.Two => "2",
                Quality.Three => "3",
                Quality.Four => "4",
                Quality.Five => "5",
                Quality.Six => "6",
                Quality.Seven => "7",
                Quality.Eight => "8",
                Quality.Nine => "9",
                Quality.Ten => "10",
                _ => throw new ArgumentOutOfRangeException(nameof(quality), quality, null)
            };
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

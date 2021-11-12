using System;
using System.Collections.Generic;
using static Pharaoh.Helpers.SceneBuilder;

namespace Pharaoh.GameInstance
{
    class Game
    {
        public Stack<ICard> Deck;
        public IPlayer Winner;
        public IPlayer Player;
        public IPlayer Enemy;
        public ICard TopCard;
        public List<int> AvailablePlayerCards = new() { 0 };
        public int TurnCounter;

        //Drawing
        public int PlayerDice;
        public int EnemyDice;
        public Game()
        {
            StartScreen();
            Initialization();
            DrawingScreen();
            while (Winner == null)
            {
                GameScreen();
            }
            EndScreen();
        }

        private void StartScreen()
        {
            Graphic.Update(BuildScene(this, SceneType.Start));
        }
        private void DrawingScreen()
        {
            PlayerDice = 0;
            EnemyDice = 0;
            while (PlayerDice==EnemyDice)
            {
                for (int i = 0; i < 10; i++)
                {
                    DiceRoll(i % 2 == 0 ? Player : Enemy);
                    Graphic.Update(BuildScene(this, SceneType.Drawing));
                }
            }

            if (PlayerDice>EnemyDice)
            {
                Player.IsFirstMoving = true;
            }
            else
            {
                Enemy.IsFirstMoving = true;
            }

            Input.WaitInput();
        }
        private void GameScreen()
        {
            throw new NotImplementedException();
        }
        private void EndScreen()
        {
            throw new NotImplementedException();
        }
        private void Initialization()
        {
            Player = new Player(Input.GetPlayerName(),isAi:false);
            Enemy = new Player("AI",isAi:true);
            Deck = new Stack<ICard>(DeckBuilder.Build(new List<ICard>(), 36));
            Winner = null;
        }

        private void DiceRoll(IPlayer player)
        {
            var rnd = new Random();
            var diceValue = rnd.Next(6) + 1;
            if (player.ID == Player.ID)
            {
                PlayerDice = diceValue;
            }
            else if (player.ID == Enemy.ID)
            {
                EnemyDice = diceValue;
            }
            else
            {
                throw new Exception($"Player with that id:{player.ID} is not exist");
            }
        }
    }
}

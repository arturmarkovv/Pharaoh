using System;
using System.Collections.Generic;
using static Pharaoh.Helpers.SceneBuilder;

namespace Pharaoh.GameInstance
{
    public class Game
    {
        public Stack<ICard> deck;
        public IPlayer player;
        public IPlayer enemy;
        public ICard topCard;
        public List<int> availablePlayerCards = new() { 0 };
        public int turnCounter;
        public Game()
        {
            //Initialization();
            DrawScreen(this,SceneType.Start);
            Input.GetPlayerName();
            Distribution();
            IPlayer winner = null;
            while (winner == null)
            {
                GameProcess();
            }
            GameEnd();
        }

        private void GameEnd()
        {
            throw new NotImplementedException();
        }

        private void GameProcess()
        {
            throw new NotImplementedException();
        }

        private void Distribution()
        {
            throw new NotImplementedException();
        }

        private void Initialization()
        {
            //var playerName = Console.ReadLine();
            //if (string.IsNullOrEmpty(playerName))
            //{
            //    playerName = "Player";
            //}

            //player = new Player(playerName);
            //ai = new Player();
            //deck = new Stack<Card>(DeckBuilder.Build(new List<Card>(), 36));
        }
    }
}

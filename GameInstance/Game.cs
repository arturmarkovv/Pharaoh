using System;
using System.Collections.Generic;
using System.Linq;
using static Pharaoh.Helpers.SceneBuilder;

namespace Pharaoh.GameInstance
{
    internal class Game
    {
        public Stack<ICard> Deck;
        public IPlayer Winner;
        public IPlayer Player;
        public IPlayer Enemy;

        public IPlayer First;
        public IPlayer Second;

        public ICard TopCard;
        public List<int> AvailablePlayerCards = new() { 0 };
        public int TurnCounter;
        public bool IsSuitChangingTurn = false;

        //Drawing
        public int PlayerDice;
        public int EnemyDice;
        public Game()
        {
            StartScreen();
            Initialization();
            DrawingScreen();
            Distribution();
            do GameScreen(); while (Winner == null);
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
            while (PlayerDice == EnemyDice)
            {
                for (int i = 0; i < 5; i++)
                {
                    DiceRoll(i % 2 == 0 ? Player : Enemy);
                    Graphic.Update(BuildScene(this, SceneType.Drawing));
                }
            }

            if (PlayerDice > EnemyDice)
            {
                Player.IsFirstMoving = true;
            }
            else
            {
                Enemy.IsFirstMoving = true;
            }

            Input.WaitInput();
        }
        private void Distribution()
        {
            if (Player.IsFirstMoving)
            {
                First = Player;
                Second = Enemy;
            }
            else
            {
                First = Enemy;
                Second = Player;
            }

            TopCard = Deck.Pop();
            TopCard.ChangeOwner(Second.ID);

            for (var i = 0; i < 3; i++)
            {
                Second.GetCard(Deck.Pop());
            }
            for (var i = 0; i < 4; i++)
            {
                First.GetCard(Deck.Pop());
            }
        }
        private void GameScreen()
        {

            TurnCounter = 0;
            while (First.HandCards.Count != 0 && Second.HandCards.Count != 0)
            {
                ReshuffleCheck();
                //AvailablePlayerCards = GetAvailableMoves(Player.HandCards);
                var curPlayer = TurnCounter % 2 == 0 ? First : Second;
                Graphic.Update(BuildScene(this, SceneType.Game));
                TurnProcess(curPlayer);
                Graphic.Update(BuildScene(this, SceneType.Game));
                TurnCounter++;
            }
            Winner = First.HandCards.Count == 0 ? First : Second;

        }
        
        private void TurnProcess(IPlayer curPlayer)
        {
            var inputType = curPlayer.IsAi ? InputType.AiSelectCard : InputType.PlSelectCard;
            var index = Input.GetInput(inputType, GetAvailableMoves(curPlayer.HandCards));

            if (index == 0)
            {
                curPlayer.GetCard(Deck.Pop());
                Graphic.Update(BuildScene(this,SceneType.Game));
            }
            else
            {
                index = curPlayer.IsAi ? 
                    Input.GetInput(InputType.AiSelectCard, GetAvailableMoves(curPlayer.HandCards)) : 
                    Input.GetValidPlayerInput(index, GetAvailableMoves(curPlayer.HandCards));
                
                Turn(curPlayer, index);
                Graphic.Update(BuildScene(this, SceneType.Game));
                return;
            }
            index = Input.GetInput(inputType, GetAvailableMoves(curPlayer.HandCards));
            if (index == 0) return;
            index = curPlayer.IsAi ?
                Input.GetInput(InputType.AiSelectCard, GetAvailableMoves(curPlayer.HandCards)) : 
                Input.GetValidPlayerInput(index, GetAvailableMoves(curPlayer.HandCards));
            Turn(curPlayer, index);
            Graphic.Update(BuildScene(this, SceneType.Game));
        }

        private void Turn(IPlayer curPlayer, int index)
        {
            var moveCard = curPlayer.MoveCard(index);
            var currentRule = Rules.RuleCheck(TopCard, moveCard);
            switch (currentRule)
            {
                case Rule.Draw:
                    GetPlayerById((Guid)TopCard.Owner).GetCard(Deck.Pop());
                    break;
                case Rule.DoubleDraw:
                    GetPlayerById((Guid)TopCard.Owner).GetCard(new[]
                    {
                        Deck.Pop(), 
                        Deck.Pop()
                    });
                    break;
                case Rule.FiveDraw:
                    GetPlayerById((Guid)TopCard.Owner).GetCard(new[]
                    {
                        Deck.Pop(),
                        Deck.Pop(),
                        Deck.Pop(),
                        Deck.Pop(),
                        Deck.Pop(),
                    });
                    break;
                case Rule.SkipTurn:
                    TurnCounter++;
                    break;
                case Rule.ChangeSuit:
                    TopCard = moveCard;
                    IsSuitChangingTurn = true;
                    moveCard = ChangeCardSuit(curPlayer);
                    IsSuitChangingTurn = false;
                    
                    break;
                case Rule.Void:
                    break;
            }
            TopCard = moveCard;
            Graphic.Update(BuildScene(this, SceneType.Game));
        }
        private ICard ChangeCardSuit(IPlayer curPlayer)
        {
            if (curPlayer.ID != Player.ID)
            {
                return UpdateTopCard(Input.GetInput(InputType.AiSelectSuit));
            }
            Graphic.Update(BuildScene(this, SceneType.Game));
            var choose = Input.GetInput(InputType.PlSelectSuit);
            return UpdateTopCard(choose);
        }

        private ICard UpdateTopCard(int changeSuitChoose)
        {
            var suits = Enum.GetValues<Suit>();
            var card = TopCard;
            card.ChangeCardSuit(suits[changeSuitChoose]);
            return card;
        }

        private IPlayer GetPlayerById(Guid id)
        {
            return Player.ID == id ? Player : Enemy;
        }

        private List<int> GetAvailableMoves(List<ICard> curPlayerHand)
        {
            var availableCards = curPlayerHand.Where(IsAvailableCard).ToList();
            return availableCards.Count == 0 ? 
                new List<int> {0} : 
                availableCards.Select(x => curPlayerHand.IndexOf(x)).ToList();
            
        }
        private bool IsAvailableCard(ICard card)
        {
            return card.Quality == TopCard.Quality || card.Suit == TopCard.Suit || card.Quality == Quality.Queen;
        }

        private void ReshuffleCheck()
        {
            if (Deck.Count != 0) return;
            var excludedCards = new List<ICard>();
            excludedCards.AddRange(First.HandCards);
            excludedCards.AddRange(Second.HandCards);
            excludedCards.Add(TopCard);
            Deck = DeckBuilder.Build(excludedCards, 36);
            Graphic.Update(BuildScene(this, SceneType.Game));
        }

        private void EndScreen()
        {
            throw new NotImplementedException();
        }
        private void Initialization()
        {
            Player = new Player(Input.GetPlayerName(), isAi: false);
            Enemy = new Player("AI", isAi: true);
            Deck = new Stack<ICard>(DeckBuilder.Build(new List<ICard>(), 36));
            Winner = null;
            TurnCounter = 0;
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

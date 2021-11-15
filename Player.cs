using System;
using System.Collections.Generic;

namespace Pharaoh
{
    public class Player : IPlayer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsAi { get; set; }
        public bool IsFirstMoving { get; set; }
        public List<ICard> HandCards { get; set; }

        public Player(string name, bool isAi)
        {
            ID = Guid.NewGuid();
            Name = name;
            IsAi = isAi;
            HandCards = new List<ICard>();
            IsFirstMoving = false;
        }
        public void GetCard(ICard card)
        {
            HandCards.Add(card);
        }

        public void GetCard(ICard[] cards)
        {
            HandCards.AddRange(cards);
        }

        public ICard MoveCard(int index)
        {
            if (index < 0 || index >= HandCards.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            var card = HandCards[index];
            card.ChangeOwner(ID);
            HandCards.Remove(card);
            return card;
        }

        public Suit ChooseSuit(int index)
        {
            if (index < 0 || index >= Enum.GetValues<Suit>().Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Enum.GetValues<Suit>()[index];
        }
    }
}
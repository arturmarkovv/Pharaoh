using System;
using System.Collections.Generic;

namespace Pharaoh
{
    public class Player : IPlayer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsAi { get; set; }
        public List<ICard> HandCards { get; set; }

        public Player(string name, bool isAi)
        {
            ID = new Guid();
            Name = name;
            IsAi = isAi;
            HandCards = new List<ICard>();
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
            return HandCards[index];
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
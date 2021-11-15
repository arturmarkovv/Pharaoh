using System;

namespace Pharaoh
{
    public class Card : ICard
    {
        public Quality Quality { get;}
        public Suit Suit { get; private set; }
        public Guid? Owner { get; private set; }

        public bool Equals(ICard second)
        {
            return Quality == second.Quality && Suit == second.Suit;
        }

        public void ChangeOwner(Guid newOwnerGuid)
        {
            Owner = newOwnerGuid;
        }

        public void ChangeCardSuit(Suit suit)
        {
            Suit = suit;
        }

        public Card(Quality quality, Suit suit, Guid? owner)
        {
            Quality = quality;
            Suit = suit;
            Owner = owner;
        }
    }
}
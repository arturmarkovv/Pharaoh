using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharaoh.Helpers;

namespace Pharaoh
{
    public static class DeckBuilder
    {
        private static readonly Quality[] Qualities = Enum.GetValues<Quality>();
        private static readonly Suit[] Suits = Enum.GetValues<Suit>();
        
        public static Stack<ICard> Build(List<ICard> excludedCards, int len = 52)
        {
            if (len > 52) len = 52;
            if (len < 24) len = 24;

            var deck = new List<ICard>();

            for (var q = (52 - len) / 4; q < Qualities.Length; q++)
            {
                deck.AddRange(Suits.Select(t => new Card(  Qualities[q], t, null)));
            }

            var forDeleting = (
                from deletingCard in excludedCards 
                from deckCard in deck 
                where deckCard.Equals(deletingCard) 
                select deckCard)
                .ToList();

            deck.RemoveAll(x => forDeleting.Contains(x));
            return new Stack<ICard>(deck.Shuffle());
        }
    }
}

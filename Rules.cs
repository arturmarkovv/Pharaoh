using System.Data;

namespace Pharaoh
{
    public static class Rules
    {
        public static Rule RuleCheck(ICard topCard, ICard turnCard)
        {
            switch (turnCard.Quality)
            {
                case Quality.Six:
                    return Rule.Draw;
                case Quality.Seven:
                    return Rule.DoubleDraw;
                case Quality.Queen:
                    return Rule.ChangeSuit;
                case Quality.King:
                    if (topCard.Suit == Suit.Spades && turnCard.Suit == Suit.Spades)
                    {
                        return Rule.FiveDraw;
                    }
                    else
                    {
                        return Rule.Void;
                    }

                case Quality.Ace:
                    return Rule.SkipTurn;
                default:
                    return Rule.Void;
            }
            
        }
    }
    public enum Rule
    {
        Draw,
        DoubleDraw,
        FiveDraw,
        SkipTurn,
        ChangeSuit,
        Void
    }
}
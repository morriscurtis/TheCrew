using MarkusCrew.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Players
{
    public class Trick
    {
        public IEnumerable<Card> Cards { get; init; }
        public Card WinningCard { get; init; }

        public Trick(IEnumerable<Card> cards, Card? winningCard)
        {
            Cards = cards;
            WinningCard = winningCard ?? throw new ArgumentNullException(nameof(winningCard));
        }
    }
}

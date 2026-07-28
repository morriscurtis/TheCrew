using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Cards
{
    public record class Card(int number, Suit suit)
    {
        public int number = number;
        public Suit suit = suit;

        public bool Beats(Card? otherCard)
        {
            if (otherCard == null)
            { 
                return true; 
            }

            if(otherCard.suit.Equals(suit))
            {
                return number > otherCard.number;
            }

            if(suit.Type.Equals(SuitType.Rocket) ) 
            { 
                return true; 
            }

            return false;
        }

        public virtual bool Equals(Card? card)
        {
            return card is not null &&
                   number == card.number &&
                   EqualityComparer<Suit>.Default.Equals(suit, card.suit);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(number, suit);
        }

        public override string? ToString()
        {
            return suit.ToString() + $" {number} ";
        }
    }
}

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

            if(otherCard.suit == suit)
            {
                return number > otherCard.number;
            }

            if(suit == Suit.Rocket) 
            { 
                return true; 
            }

            return false;
        }
    }
}

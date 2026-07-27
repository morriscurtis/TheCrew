using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Cards
{
    public static class Deck
    {
        private static List<Card> AllCards = new List<Card>(40);

        static Deck()
        {
            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                int amount = 9;
                if (suit == Suit.Rocket)
                {
                    amount = 4;
                }

                for (int i = 1; i <= amount; i++) 
                {
                    AllCards.Add(new Card(i, suit));
                }
            }
        }

        public static IEnumerable<Card> GetShuffledCards()
        {
            return AllCards.Shuffle();
        }

        public static Card GetRandomCard()
        {
            int rng = Random.Shared.Next(0, AllCards.Count);
            return AllCards[rng];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MarkusCrew.Cards
{
    public static class Deck
    {
        private static List<Card> AllCards = new List<Card>(40);

        public static Dictionary<SuitType, Suit> AllSuits = new Dictionary<SuitType, Suit>(5);

        static Deck()
        {
            CreateSuits();
            foreach (SuitType suitType in Enum.GetValues<SuitType>())
            {
                int amount = 9;
                if (suitType == SuitType.Rocket)
                {
                    amount = 4;
                }

                for (int i = 1; i <= amount; i++) 
                {
                    AllCards.Add(new Card(i, AllSuits[suitType]));
                }
            }
        }

        private static void CreateSuits()
        {
            AllSuits = new Suit[]
            {
                new(SuitType.Rocket, "R", ConsoleColor.Black),
                new(SuitType.Blue, "B", ConsoleColor.Blue),
                new(SuitType.Green, "G", ConsoleColor.Green),
                new(SuitType.Yellow, "Y", ConsoleColor.Yellow),
                new(SuitType.Pink, "P", ConsoleColor.Magenta),
                
            }.ToDictionary(s => s.Type);
        }

        public static IEnumerable<Card> GetShuffledCards()
        {
            return AllCards.Shuffle().ToList();
        }

        public static Card GetRandomCard()
        {
            int rng = Random.Shared.Next(0, AllCards.Count);
            return AllCards[rng];
        }
    }
}

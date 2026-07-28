using MarkusCrew.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Players
{
    internal class Player(int position, List<Card> cards)
    {
        public int Position { get; init; } = position;

        public List<Card> Cards { get; init; } = cards;

        public List<Card> PlayedCards { get; } = new List<Card>();

        public List<Trick> Tricks { get; } = new List<Trick>();

        public IEnumerable<Card> CurrentHand => Cards.Except(PlayedCards);

        public void AddTrick(Trick trick)
        {
            Tricks.Add(trick);
        }

        public Card PlayCard(Card? card)
        {
            var selectedCard = SelectCardToPlay(card);
            PlayedCards.Add(selectedCard);
            return selectedCard;
        }

        private Card SelectCardToPlay(Card? card)
        {
            if(card == null)
            {
                return CurrentHand.First();
            }

            var currentHand = CurrentHand;
            var availableCards = currentHand.Where(item => item.suit == card.suit).ToList();
            if (availableCards.Any())
            {
                return availableCards.First();
            }

            return currentHand.First();
        }

        public override bool Equals(object? obj)
        {
            return obj is Player player &&
                   Position == player.Position;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position);
        }
    }
}

using MarkusCrew.Cards;
using MarkusCrew.Players;

namespace MarkusCrew.Game
{
    internal class Round(IList<Player> players, int number, Player startingPlayer)
    {
        public IList<Player> Players { get; init; } = players;
        public int Number { get; init; } = number;
        public Player StartingPlayer { get; init; } = startingPlayer;

        public Player? RoundWinner { get; private set; }

        public Trick? Trick { get; private set; }

        public void PlayRound()
        {
            int startingIndex = Players.IndexOf(StartingPlayer);

            Card? currentCardToBeat = null;
            List<Card> playedCards = new List<Card>(Players.Count);

            for (int i = startingIndex, j = 0; j < Players.Count; j++)
            {
                Player currentPlayer = Players[(i + j) % Players.Count];
                Card selectedCard = currentPlayer.PlayCard(currentCardToBeat);
                playedCards.Add(selectedCard);
                if(selectedCard.Beats(currentCardToBeat))
                {
                    currentCardToBeat = selectedCard;
                    RoundWinner = currentPlayer;
                }
            }

            Trick trick = new Trick(playedCards, currentCardToBeat);
            Trick = trick;
            RoundWinner?.AddTrick(trick);
        }
    }
}

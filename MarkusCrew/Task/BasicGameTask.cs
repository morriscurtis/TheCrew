using MarkusCrew.Cards;
using MarkusCrew.Players;
using MarkusCrew.Game;

namespace MarkusCrew.Task
{
    internal class BasicGameTask(Card card, Player player) : GameTask
    {
        public Card Card { get; init; } = card;

        public Player Player { get; init; } = player;

        public IEnumerable<Round> CompletedRounds { get; private set; } = Enumerable.Empty<Round>();

        public bool IsCompleted { get
            {
                foreach (var round in CompletedRounds)
                {
                    if (round.Trick != null && round.Trick.Cards.Contains(Card) && round.RoundWinner == Player)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        public void CompleteRounds(IEnumerable<Round> rounds) {
            CompletedRounds = rounds;        
        }
    }
}

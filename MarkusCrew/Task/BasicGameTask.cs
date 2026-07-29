using MarkusCrew.Cards;
using MarkusCrew.Players;
using MarkusCrew.Game;

namespace MarkusCrew.Task
{
    internal class BasicGameTask(Card card, Player player, List<Round> completedRounds) : GameTask(completedRounds)
    {
        public Card Card { get; init; } = card;

        public Player Player { get; init; } = player;

        public override bool IsCompleted { get
            {
                foreach (var round in CompletedRounds)
                {
                    if (round.Trick != null && round.Trick.Cards.Contains(Card) && round.RoundWinner.Equals(Player))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}

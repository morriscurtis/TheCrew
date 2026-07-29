using MarkusCrew.Cards;
using MarkusCrew.Game.Missions.Options;
using MarkusCrew.Players;
using MarkusCrew.Task;

namespace MarkusCrew.Game.Missions
{
    internal class Mission(MissionOptions options, ITaskFactory taskFactory) : IMission
    {
        public MissionOptions Options { get; } = options;
        public ITaskFactory TaskFactory { get; } = taskFactory;

        private List<Player> players = new List<Player>(4);

        private List<Round> rounds = new List<Round>();

        private List<GameTask> tasks = new List<GameTask>();

        private Player initialStartPlayer;

        public bool SimulateInstance()
        {
            InitializePlayers();
            SimulateRounds();
            InitializeTasks();
            return tasks.All(item => item.IsCompleted);
        }

        private void InitializeTasks()
        {
            tasks = TaskFactory.CreateTasksForMission(Options.MissionIds.First(), players, rounds);
        }

        private void SimulateRounds()
        {
            Player startPlayer = initialStartPlayer;
            for (int i = 0; i < 10; i++)
            {
                Round round = new Round(players, 1, startPlayer);
                rounds.Add(round);
                round.PlayRound();
                startPlayer = round.RoundWinner;
            }
        }

        private void InitializePlayers()
        {
            IEnumerable<Card> allCards = Deck.GetShuffledCards();
            Card startCard = new Card(4, Deck.AllSuits[SuitType.Rocket]);
            for (int i = 0; i < 4; i++)
            {
                Range range = new Range(i * 10, i * 10 + 10);
                Player newPlayer = new Player(i, allCards.Take(range).ToList());
                players.Add(newPlayer);
                if (newPlayer.Cards.Contains(startCard))
                {
                    initialStartPlayer = newPlayer;
                }
            }
        }
    }
}

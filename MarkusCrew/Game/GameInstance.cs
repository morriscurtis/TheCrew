using MarkusCrew.Cards;
using MarkusCrew.Players;
using MarkusCrew.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Game
{
    internal class GameInstance(int missionId)
    {
        public int MissionId { get; } = missionId;

        private List<Player> players = new List<Player>(4);

        private List<Round> rounds = new List<Round>();

        private List<BasicGameTask> tasks = new List<BasicGameTask>();

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
            
            BasicGameTask task = new BasicGameTask(Deck.GetRandomCard(), players[Random.Shared.Next(0,4)]);
            task.CompleteRounds(rounds);
            tasks.Add(task);
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
                if(newPlayer.Cards.Contains(startCard))
                {
                    initialStartPlayer = newPlayer;
                }
            }
        }
    }
}

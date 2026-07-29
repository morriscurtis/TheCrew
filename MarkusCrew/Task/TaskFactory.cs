using MarkusCrew.Cards;
using MarkusCrew.Game;
using MarkusCrew.Players;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MarkusCrew.Task
{
    internal class TaskFactory() : ITaskFactory
    {
        public List<GameTask> CreateTasksForMission(int missionId, List<Player> players, List<Round> rounds)
        {
            switch(missionId)
            {
                case 1:
                    return TasksForMission1(players, rounds);
                case 2:
                    return TasksForMission2(players, rounds);

                default: return new List<GameTask>();
            }
        }

        private List<GameTask> TasksForMission1(List<Player> players, List<Round> rounds)
        {
            BasicGameTask task = new BasicGameTask(Deck.GetRandomCards(1).First(), players[Random.Shared.Next(0, 4)], rounds);
            return [task];
        }

        private List<GameTask> TasksForMission2(List<Player> players, List<Round> rounds)
        {
            List<GameTask> tasks = new List<GameTask>(2);
            var cards = Deck.GetRandomCards(2);
            int player1 = Random.Shared.Next(0, 4);
            int player2 = (player1 + 1) % 4;
            tasks.Add(new BasicGameTask(cards[0], players[player1], rounds));
            tasks.Add(new BasicGameTask(cards[1], players[player2], rounds));
            return tasks;
        }
    }
}

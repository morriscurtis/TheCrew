using MarkusCrew.Game;
using MarkusCrew.Players;

namespace MarkusCrew.Task
{
    internal interface ITaskFactory
    {
        public List<GameTask> CreateTasksForMission(int MissionId, List<Player> players, List<Round> rounds);
    }
}
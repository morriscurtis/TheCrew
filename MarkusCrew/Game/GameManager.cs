using MarkusCrew.Game.Missions;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusCrew.Game
{
    internal class GameManager(IServiceProvider services)
    {
        public IServiceProvider Services { get; } = services;

        public int SimulateMission()
        {
            int counter = 0;
            bool retry = true;
            while (retry) {
                counter++;
                IMission mission = Services.GetService<IMission>();
                retry = !mission.SimulateInstance();
            }

            return counter;
        }
    }
}

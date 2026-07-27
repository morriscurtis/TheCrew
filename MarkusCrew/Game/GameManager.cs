using MarkusCrew.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Game
{
    internal class GameManager(int missionId)
    {
        public int MissionId { get; } = missionId;

        public int SimulateMission()
        {
            int counter = 0;
            bool retry = true;
            while (retry) {
                counter++;
                GameInstance instance = new GameInstance(missionId);
                retry = !instance.SimulateInstance();
            }

            return counter;
        }
    }
}

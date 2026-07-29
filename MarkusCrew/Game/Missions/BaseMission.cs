using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Game.Missions
{
    internal abstract class BaseMission : IMission
    {
        public int MissionId => throw new NotImplementedException();

        public bool SimulateInstance()
        {
            throw new NotImplementedException();
        }
    }
}

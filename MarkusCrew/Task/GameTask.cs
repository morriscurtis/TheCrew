using MarkusCrew.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkusCrew.Task
{
    internal abstract class GameTask(List<Round> completedRounds)
    {
        public List<Round> CompletedRounds { get; } = completedRounds;

        public abstract bool IsCompleted { get; }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class SeparationService
    {
        private readonly IGetNetwork _programmers;
        private readonly List<string> _visited;
        private readonly Queue<string> _queue;

        public SeparationService(IGetNetwork getNetwork)
        {
            _programmers = getNetwork;
            _visited = new List<string>();
            _queue = new Queue<string>();
        }

        public int GetDegreesBetween(string id, string goalId)
        {
            if (id == goalId)
                return -1;

            var goalProgrammer = _programmers.GetById(goalId);

            _queue.Enqueue(id);
            var depth = 0;

            while (_queue.Count > 0)
            {
                var currentId = _queue.Dequeue();
                _visited.Add(currentId);
                var currentProgrammer = _programmers.GetById(currentId);

                if (AreDirectlyRelated(currentProgrammer, goalProgrammer))
                    return depth;

                var recommenders = currentProgrammer.GetRecommenders(_programmers.GetAll()).Select(p => p.GetId());
                var neighbours = currentProgrammer.GetRecommendations().Concat(recommenders);

                foreach (var neighbour in neighbours.Where(n => !_visited.Contains(n)))
                    _queue.Enqueue(neighbour);

                ++depth;
            }

            return -1;
        }

        private static bool AreDirectlyRelated(IRecommend programmerA, IRecommend programmerB)
        {
            return programmerA
                   .GetRecommendations()
                   .Contains(programmerB.GetId())
                || programmerB
                   .GetRecommendations()
                   .Contains(programmerA.GetId());
        }
    }
}
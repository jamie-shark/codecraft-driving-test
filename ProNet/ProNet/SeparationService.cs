using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class SeparationService : ISeparationService
    {
        private readonly INetworkRepository _programmers;
        private readonly List<string> _visited;
        private readonly Queue<Node> _queue;

        public SeparationService(INetworkRepository networkRepository)
        {
            _programmers = networkRepository;
            _visited = new List<string>();
            _queue = new Queue<Node>();
        }

        public int GetDegreesBetween(string id, string goalId)
        {
            if (id == goalId)
                return 0;

            var goalProgrammer = _programmers.GetById(goalId);

            _queue.Enqueue(new Node(id, 1));

            while (_queue.Count > 0)
            {
                var currentNode = _queue.Dequeue();

                _visited.Add(currentNode.Id);

                var currentProgrammer = _programmers.GetById(currentNode.Id);

                if (AreDirectlyRelated(currentProgrammer, goalProgrammer))
                    return currentNode.Depth;

                var recommenders = currentProgrammer.GetRecommenders(_programmers.GetAll()).Select(p => p.GetId());
                var neighbours = currentProgrammer
                    .GetRecommendations()
                    .Concat(recommenders);

                foreach (var neighbour in neighbours)
                    if (!_visited.Contains(neighbour))
                        _queue.Enqueue(new Node(neighbour, currentNode.Depth + 1));
            }

            return 0;
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

    internal class Node
    {
        public string Id { get; }
        public int Depth { get; }

        public Node(string id, int depth)
        {
            Id = id;
            Depth = depth;
        }

    }
}
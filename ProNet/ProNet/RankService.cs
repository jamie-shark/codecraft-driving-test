using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class RankService : IRankService
    {
        private int _iteration;
        private readonly IEnumerable<IProgrammer> _programmers;

        public RankService(IProgrammerRepository programmerRepository)
        {
            _programmers = programmerRepository.GetAll().ToArray();
        }

        public double GetRank(string programmerId)
        {
            const int settleLimit = 20;
            const double dampingFactor = 0.85d;

            while (++_iteration < settleLimit)
                foreach (var programmer in _programmers)
                    programmer.Rank = 1 - dampingFactor + dampingFactor * programmer.GetRecommenders(_programmers).Select(p => GetRank(p.GetId()) / RecommendationCount(p)).Sum();

            try
            {
                return _programmers.Single(programmer => programmer.GetId() == programmerId).Rank;
            }
            catch (InvalidOperationException e)
            {
                throw new ArgumentException($"Programmer {programmerId} was not found");
            }
        }

        private static int RecommendationCount(IRankable programmer)
        {
            return programmer.GetRecommendations().Count();
        }
    }
}
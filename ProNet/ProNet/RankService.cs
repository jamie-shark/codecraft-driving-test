using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class RankService : IRankService
    {
        private int _iteration;
        private readonly IGetNetwork _getNetwork;
        private readonly List<IProgrammer> _programmers;

        public RankService(IGetNetwork getNetwork)
        {
            _getNetwork = getNetwork;
            _programmers = _getNetwork.GetAll().ToList();
        }

        public double GetRank(string programmerId)
        {
            const int settleLimit = 20;

            _getNetwork.GetById(programmerId);

            while (++_iteration < settleLimit)
                foreach (var eachProgrammer in _programmers)
                    eachProgrammer.Rank = NewRank(eachProgrammer);

            return _programmers.Single(p => p.GetId() == programmerId).Rank;
        }

        private double NewRank(IRecommended eachProgrammer)
        {
            const double dampingFactor = 0.85d;
            return 1 - dampingFactor + dampingFactor * eachProgrammer
                .GetRecommenders(_programmers)
                .Select(p => GetRank(p.GetId()) / RecommendationCount(p))
                .Sum();
        }

        private static int RecommendationCount(IRecommend programmer)
        {
            return programmer.GetRecommendations().Count();
        }
    }
}
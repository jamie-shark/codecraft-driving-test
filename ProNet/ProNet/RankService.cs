using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class RankService : IRankService
    {
        private int _iteration;
        private readonly IGetProgrammers _getProgrammers;
        private readonly List<IProgrammer> _programmers;

        public RankService(IGetProgrammers getProgrammers)
        {
            _getProgrammers = getProgrammers;
            _programmers = _getProgrammers.GetAll().ToList();
        }

        public double GetRank(string programmerId)
        {
            const int settleLimit = 20;
            const double dampingFactor = 0.85d;

            _getProgrammers.GetById(programmerId);

            while (++_iteration < settleLimit)
                foreach (var eachProgrammer in _programmers)
                    eachProgrammer.Rank = 1 - dampingFactor + dampingFactor * eachProgrammer.GetRecommenders(_programmers).Select(p => GetRank(p.GetId()) / RecommendationCount(p)).Sum();

            return _programmers.Single(p => p.GetId() == programmerId).Rank;
        }

        private static int RecommendationCount(IRankable programmer)
        {
            return programmer.GetRecommendations().Count();
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class ProgrammerRankCalculator : IRankCalculator
    {
        private readonly IProgrammerRepository _programmerRepository;

        private int _iteration;

        public ProgrammerRankCalculator(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public double GetRank(string programmerId)
        {
            const int settleLimit = 40;
            const double dampingFactor = 0.85d;

            var rank = 0d;
            var programmer = _programmerRepository.GetById(programmerId);

            while (++_iteration < settleLimit)
                rank = (1 - dampingFactor) + dampingFactor * SumOfOthers(programmer);

            return rank;
        }

        private double SumOfOthers(IRankable programmer)
        {
            return OthersThatRecommend(programmer)
                .Select(p => GetRank(p.GetId()) / RecommendationCount(p))
                .Sum();
        }

        private IEnumerable<IRankable> OthersThatRecommend(IRankable programmer)
        {
            return programmer.GetRecommenders(_programmerRepository.GetAll());
        }

        private static int RecommendationCount(IRankable programmer)
        {
            return programmer.GetRecommendations().Count();
        }
    }
}
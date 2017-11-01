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

        public double GetRank(string programmerId, int settleLimit)
        {
            const double dampingFactor = 0.85d;
            var rank = 0d;
            var page = _programmerRepository.GetById(programmerId);

            while (++_iteration < settleLimit)
                rank = (1 - dampingFactor) + dampingFactor * OthersThatReference(page)
                            .Select(p => GetRank(p.GetId(), settleLimit) / ReferenceCount(p))
                            .Sum();

            return rank;
        }

        private IEnumerable<IRankable> OthersThatReference(IRankable programmer)
        {
            return programmer.GetRecommenders(_programmerRepository.GetAll());
        }

        private static int ReferenceCount(IRankable programmer)
        {
            return programmer.GetRecommendations().Count();
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class ProgrammerRankCalculator : IRankCalculator
    {
        private readonly IProgrammerRepository _programmerRepository;
        private readonly int _settleLimit;

        private int _iteration;

        public ProgrammerRankCalculator(IProgrammerRepository programmerRepository, int settleLimit)
        {
            _programmerRepository = programmerRepository;
            _settleLimit = settleLimit;
        }

        public double GetRank(string programmerId)
        {
            const double dampingFactor = 0.85d;
            var rank = 0d;
            var page = _programmerRepository.GetById(programmerId);

            while (++_iteration < _settleLimit)
                rank = (1 - dampingFactor) + dampingFactor * OthersThatReference(page)
                            .Select(p => GetRank(p.GetId()) / ReferenceCount(p))
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
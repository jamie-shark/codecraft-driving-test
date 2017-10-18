using System.Linq;

namespace ProNet
{
    public class RankCalculator
    {
        private readonly IProgrammerRepository _programmerRepository;
        private readonly int _settleLimit;

        private int _iteration;

        public RankCalculator(IProgrammerRepository programmerRepository, int settleLimit)
        {
            _programmerRepository = programmerRepository;
            _settleLimit = settleLimit;
        }

        public decimal Calculate(Programmer programmer)
        {
            const decimal dampingFactor = 0.85m;
            var rank = 0m;
            var iteration = 0;

            while (++_iteration < _settleLimit)
                rank = (1 - dampingFactor) + dampingFactor * _programmerRepository
                            .GetAll()
                            .Where(p => p.Recommendations.Contains(programmer.Id))
                            .Select(p => Calculate(p) / p.Recommendations.Count)
                            .Sum();

            return rank;
        }
    }
}
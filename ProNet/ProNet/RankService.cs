using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class RankService : IRankService
    {
        private int _iteration;
        private readonly IProgrammerRepository _programmerRepository;
        private readonly List<IProgrammer> _programmers;

        public RankService(IProgrammerRepository programmerRepositoryRepository)
        {
            _programmerRepository = programmerRepositoryRepository;
            _programmers = _programmerRepository.GetAll().ToList();
        }

        public double GetRank(string programmerId)
        {
            const int settleLimit = 20;
            const double dampingFactor = 0.85d;

            _programmerRepository.GetById(programmerId);

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
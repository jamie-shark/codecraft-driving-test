using System.Collections.Generic;

namespace ProNet
{
    public class RecommendationService : IRecommendationService
    {
        private readonly NetworkRepository _programmerRepository;

        public RecommendationService(NetworkRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public IEnumerable<string> GetRecommendations(string programmerId)
        {
            return _programmerRepository.GetById(programmerId).GetRecommendations();
        }
    }
}
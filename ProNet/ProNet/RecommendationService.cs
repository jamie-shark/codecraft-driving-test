using System.Collections.Generic;

namespace ProNet
{
    public class RecommendationService : IRecommendationService
    {
        private readonly GetNetwork _programmerRepository;

        public RecommendationService(GetNetwork programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public IEnumerable<string> GetRecommendations(string programmerId)
        {
            return _programmerRepository.GetById(programmerId).GetRecommendations();
        }
    }
}
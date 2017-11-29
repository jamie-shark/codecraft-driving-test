using System.Collections.Generic;

namespace ProNet
{
    public interface IRecommendationService
    {
        IEnumerable<string> GetRecommendations(string programmerId);
    }
}
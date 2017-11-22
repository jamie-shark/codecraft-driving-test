using System.Collections.Generic;

namespace ProNet
{
    public interface IRecommend : IIdentifiable
    {
        IEnumerable<string> GetRecommendations();
    }
}
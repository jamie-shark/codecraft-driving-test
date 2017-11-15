using System.Collections.Generic;

namespace ProNet
{
    public interface IRecommend
    {
        IEnumerable<string> GetRecommendations();
    }
}
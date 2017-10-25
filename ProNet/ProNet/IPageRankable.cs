using System.Collections.Generic;

namespace ProNet
{
    public interface IPageRankable
    {
        IEnumerable<string> GetRecommendations();
        IEnumerable<IPageRankable> GetRecommenders(IEnumerable<IPageRankable> pages);
    }
}
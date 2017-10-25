using System.Collections.Generic;

namespace ProNet
{
    public interface IPageRankable
    {
        string Id { get; }
        IEnumerable<string> Recommendations { get; }
    }
}
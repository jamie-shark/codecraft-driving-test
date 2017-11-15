using System.Collections.Generic;

namespace ProNet
{
    public interface IRank
    {
        string GetId();
        IEnumerable<string> GetRecommendations();
        IEnumerable<IRank> GetRecommenders(IEnumerable<IRank> programmers);
        double Rank { get; set; }
    }
}
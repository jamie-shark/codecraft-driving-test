using System.Collections.Generic;

namespace ProNet
{
    public interface IRankable
    {
        string GetId();
        IEnumerable<string> GetRecommendations();
        IEnumerable<IRankable> GetRecommenders(IEnumerable<IRankable> programmers);
        double Rank { get; set; }
    }
}
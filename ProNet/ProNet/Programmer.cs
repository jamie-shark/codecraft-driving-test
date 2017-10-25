using System.Collections.Generic;

namespace ProNet
{
    public class Programmer : IPageRankable
    {
        public string Id { get; }
        public IEnumerable<string> Recommendations { get; }

        public Programmer(string id, IEnumerable<string> recommendations)
        {
            Id = id;
            Recommendations = recommendations;
        }
    }
}
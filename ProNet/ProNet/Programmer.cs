using System.Collections.Generic;

namespace ProNet
{
    public class Programmer
    {
        public string Id { get; }
        public List<string> Recommendations { get; }

        public Programmer(string id, List<string> recommendations)
        {
            Id = id;
            Recommendations = recommendations;
        }
    }
}
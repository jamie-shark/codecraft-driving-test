using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Programmer : IRankable
    {
        private readonly string _id;
        private readonly IEnumerable<string> _recommendations;

        public Programmer(string id, IEnumerable<string> recommendations)
        {
            _id = id;
            _recommendations = recommendations;
        }

        public string GetId()
        {
            return _id;
        }

        public IEnumerable<string> GetRecommendations()
        {
            return _recommendations;
        }

        public IEnumerable<IRankable> GetRecommenders(IEnumerable<IRankable> pages)
        {
            return pages.Where(p => p.GetRecommendations().Contains(_id));
        }
    }
}
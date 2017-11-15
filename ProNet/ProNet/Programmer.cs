using System.Collections.Generic;
using System.Linq;

namespace ProNet
{

    public interface IProgrammer : IRank, ISkill
    {
    }

    public class Programmer : IProgrammer
    {
        private readonly string _id;
        private readonly IEnumerable<string> _recommendations;
        private readonly string[] _skills;

        public double Rank { get; set; }

        public Programmer(string id, IEnumerable<string> recommendations, string[] skills)
        {
            _id = id;
            _recommendations = recommendations;
            _skills = skills;
        }

        public string GetId()
        {
            return _id;
        }

        public IEnumerable<string> GetRecommendations()
        {
            return _recommendations;
        }

        public IEnumerable<IRank> GetRecommenders(IEnumerable<IRank> programmers)
        {
            return programmers.Where(p => p.GetRecommendations().Contains(_id));
        }

        public IEnumerable<string> GetSkills()
        {
            return _skills;
        }
    }
}
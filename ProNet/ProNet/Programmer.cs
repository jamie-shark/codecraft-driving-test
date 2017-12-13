using System.Collections.Generic;
using System.Linq;

namespace ProNet
{

    public interface IProgrammer : IRank, ISkill, IRecommend, IRecommended
    {
    }

    public class Programmer : IProgrammer
    {
        private readonly string _id;
        private readonly IEnumerable<string> _recommendations;
        private readonly IEnumerable<string> _skills;

        public double Rank { get; set; }

        public Programmer(string id, IEnumerable<string> recommendations, IEnumerable<string> skills)
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

        public IEnumerable<IRecommend> GetRecommenders(IEnumerable<IRecommend> programmers)
        {
            return programmers.Where(p => p.GetRecommendations().Contains(_id));
        }

        public IEnumerable<string> GetSkills()
        {
            return _skills;
        }

        public bool HasSkill(string skill)
        {
            return _skills.Contains(skill);
        }
    }
}
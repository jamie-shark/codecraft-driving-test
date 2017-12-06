using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class SkillsService : ISkillsService
    {
        private readonly INetworkRepository _networkRepository;

        public SkillsService(INetworkRepository networkRepository)
        {
            _networkRepository = networkRepository;
        }

        public IEnumerable<string> GetSkills(string programmerId)
        {
            return _networkRepository.GetById(programmerId).GetSkills();
        }

        public int GetSkillIndex(string programmerId, string skill)
        {
            var skills = GetSkills(programmerId);
            return GetIndexOf(skill, skills) + 1 ?? 0;
        }

        private static int? GetIndexOf(string skill, IEnumerable<string> skills)
        {
            return skills
                ?.Select((item, index) => new {skill=item, index})
                .SingleOrDefault(item => item.skill == skill)
                ?.index;
        }
    }
}
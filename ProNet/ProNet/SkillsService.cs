using System.Collections.Generic;

namespace ProNet
{
    public class SkillsService
    {
        private readonly IProgrammerRepository _programmerRepository;

        public SkillsService(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public IEnumerable<string> GetSkills(string programmerId)
        {
            return _programmerRepository.GetById(programmerId).GetSkills();
        }
    }
}
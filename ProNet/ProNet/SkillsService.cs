using System;
using System.Collections.Generic;

namespace ProNet
{
    public class SkillsService : ISkillsService
    {
        private readonly IProgrammerRepository _programmerRepository;

        public SkillsService(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public IEnumerable<string> GetSkills(string programmerId)
        {
            var programmer = _programmerRepository.GetById(programmerId);
            if (programmer == null) throw new ArgumentException($"Programmer {programmerId} was not found");
            return programmer.GetSkills();
        }
    }
}
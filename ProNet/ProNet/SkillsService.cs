using System.Collections.Generic;

namespace ProNet
{
    public class SkillsService : ISkillsService
    {
        private readonly IGetProgrammers _getProgrammers;

        public SkillsService(IGetProgrammers getProgrammers)
        {
            _getProgrammers = getProgrammers;
        }

        public IEnumerable<string> GetSkills(string programmerId)
        {
            return _getProgrammers.GetById(programmerId).GetSkills();
        }
    }
}
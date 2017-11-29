using System.Collections.Generic;

namespace ProNet
{
    public class TeamStrengthService
    {
        private readonly ISeparationService _separationService;
        private readonly ISkillsService _skillService;

        public TeamStrengthService(ISeparationService separationService, ISkillsService skillService)
        {
            _separationService = separationService;
            _skillService = skillService;
        }

        public double GetStrength(string language, IEnumerable<string> team)
        {
            return 0;
        }
    }
}
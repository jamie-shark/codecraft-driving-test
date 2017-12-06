using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class TeamStrengthService
    {
        private readonly ISeparationService _separationService;
        private readonly ISkillsService _skillService;
        private readonly IRankService _rankService;

        public TeamStrengthService(ISeparationService separationService, ISkillsService skillService, IRankService rankService)
        {
            _separationService = separationService;
            _skillService = skillService;
            _rankService = rankService;
        }

        public double GetStrength(string language, IEnumerable<string> team)
        {
            return team.Any()
                ? team.Average(member => _rankService.GetRank(member))
                : 0d;
        }
    }
}
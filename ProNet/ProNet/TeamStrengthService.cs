using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class TeamStrengthService : ITeamStrengthService
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

        public double GetTeamStrength(string skill, IEnumerable<string> team)
        {
            return team.Any()
                ? Strength(skill, team)
                : 0d;
        }

        public double GetIndividualStrength(string skill, string individual)
        {
            return _rankService.GetRank(individual) / _skillService.GetSkillIndex(individual, skill);
        }

        private double Strength(string skill, IEnumerable<string> team)
        {
            var leader = team.First();
            var strength = (GetIndividualStrength(skill, leader)
                            + team.Skip(1).Sum(member =>
                                GetIndividualStrength(skill, member) / _separationService.GetDegreesBetween(leader, member))) / team.Count();
            return double.IsNaN(strength)
                ? 0d
                : strength;
        }
    }
}
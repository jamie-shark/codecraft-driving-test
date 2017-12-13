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

        public double GetIndividualStrength(string programmerId, string skill)
        {
            return RankSkillIndex(programmerId, skill);
        }

        private double Strength(string skill, IEnumerable<string> team)
        {
            var leader = team.First();
            var strength = (RankSkillIndex(leader, skill)
                            + team.Skip(1).Sum(member =>
                                RankSkillIndex(member, skill) / _separationService.GetDegreesBetween(leader, member))) / team.Count();
            return double.IsNaN(strength)
                ? 0d
                : strength;
        }

        private double RankSkillIndex(string teamMember, string skill)
        {
            return _rankService.GetRank(teamMember) / _skillService.GetSkillIndex(teamMember, skill);
        }
    }
}
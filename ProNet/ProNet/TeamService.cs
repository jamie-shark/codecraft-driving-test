using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class TeamService : ITeamService
    {
        private readonly INetworkRepository _networkRepository;
        private readonly ISeparationService _separationService;
        private readonly ISkillsService _skillService;
        private readonly IRankService _rankService;

        public TeamService(INetworkRepository networkRepository, ISeparationService separationService, ISkillsService skillService, IRankService rankService)
        {
            _networkRepository = networkRepository;
            _separationService = separationService;
            _skillService = skillService;
            _rankService = rankService;
        }

        public double GetStrength(string skill, IEnumerable<string> team)
        {
            return team.Any()
                ? Strength(skill, team)
                : 0d;
        }

        public IEnumerable<string> FindStrongestTeam(string skill, int size)
        {
            if (size == 0)
                throw new ArgumentException("Team size must be greater than zero");

            var network = _networkRepository.GetAll();
            var programmersWithSkill = network.Select(programmer => new
                    {
                        Id = programmer.GetId(),
                        SkillIndex = _skillService.GetSkillIndex(programmer.GetId(), skill)
                    })
                .Where(programmer => programmer.SkillIndex > 0);

            var ranks = programmersWithSkill.Select(programmer => new
                {
                    programmer.Id,
                    programmer.SkillIndex,
                    Rank = _rankService.GetRank(programmer.Id)
                });

            return ranks.OrderBy(programmer => programmer.SkillIndex)
                .ThenByDescending(programmer => programmer.Rank)
                .Select(programmer => programmer.Id)
                .Take(size);
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
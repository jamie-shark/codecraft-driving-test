using System;
using System.Collections.Generic;
using System.Linq;
using ProNet.Util;

namespace ProNet
{
    public class StrongestTeamService : IStrongestTeamService
    {
        private readonly INetworkRepository _networkRepository;
        private readonly ITeamStrengthService _teamStrengthService;
        private readonly IPermutationService _permutationService;
        private readonly ISeparationService _separationService;

        public StrongestTeamService(INetworkRepository networkRepository, ITeamStrengthService teamStrengthService, IPermutationService permutationService, ISeparationService separationService)
        {
            _networkRepository = networkRepository;
            _teamStrengthService = teamStrengthService;
            _permutationService = permutationService;
            _separationService = separationService;
        }

        public IEnumerable<string> FindStrongestTeam(string skill, int size)
        {
            if (size == 0)
                throw new ArgumentException("Team size must be greater than zero");

            var validProgrammers = _networkRepository
                .GetAll()
                .Where(programmer => programmer.HasSkill(skill))
                .Select(programmer => programmer.GetId());

            var possibleTeams = _permutationService.GetPermutations(validProgrammers, size);

            var possibleTeamsWithLeadersChosen = new List<IEnumerable<string>>();
            foreach (var possibleTeam in possibleTeams)
            {
                var leaderScores = new List<Tuple<string, int>>();
                foreach (var member in possibleTeam)
                {
                    var members = possibleTeam.Where(item => item != member);
                    var degreesFromTeamMembers =
                        members.Select(item => _separationService.GetDegreesBetween(member, item));
                    if (degreesFromTeamMembers.Contains(0))
                        continue;

                    leaderScores.Add(new Tuple<string, int>(member, degreesFromTeamMembers.Sum()));
                }
                if (!leaderScores.Any())
                    continue;

                var leader = leaderScores.OrderBy(score => score.Item2).First().Item1;
                possibleTeamsWithLeadersChosen.Add(new[] { leader }.Concat(possibleTeam.Where(item => item != leader)));
            }

            return possibleTeamsWithLeadersChosen.OrderByDescending(team => _teamStrengthService.GetTeamStrength(skill, team)).First();
        }
    }
}
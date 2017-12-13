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

        public StrongestTeamService(INetworkRepository networkRepository, ITeamStrengthService teamStrengthService, IPermutationService permutationService)
        {
            _networkRepository = networkRepository;
            _teamStrengthService = teamStrengthService;
            _permutationService = permutationService;
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

            return possibleTeams
                .OrderByDescending(possibleTeam => _teamStrengthService.GetStrength(skill, possibleTeam))
                .First()
                .OrderByDescending(teamMember => _teamStrengthService.GetMemberStrength(teamMember, skill));
        }
    }
}
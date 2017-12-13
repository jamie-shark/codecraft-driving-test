using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class StrongestTeamService : IStrongestTeamService
    {
        private readonly INetworkRepository _networkRepository;
        private readonly ITeamStrengthService _teamStrengthService;

        public StrongestTeamService(INetworkRepository networkRepository, ITeamStrengthService teamStrengthService)
        {
            _networkRepository = networkRepository;
            _teamStrengthService = teamStrengthService;
        }

        public IEnumerable<string> FindStrongestTeam(string skill, int size)
        {
            if (size == 0)
                throw new ArgumentException("Team size must be greater than zero");

            var validProgrammers = _networkRepository
                .GetAll()
                .Where(programmer => programmer.HasSkill(skill))
                .Select(programmer => programmer.GetId());

            var possibleTeams = GetAllPermutations(validProgrammers, size);

            return possibleTeams
                .OrderByDescending(possibleTeam => _teamStrengthService.GetStrength(skill, possibleTeam))
                .First();
        }

        private static IEnumerable<IEnumerable<string>> GetAllPermutations(IEnumerable<string> set, int size)
        {
            if (size == 1)
                return set.Select(item => new List<string> {item}.AsEnumerable());

            return set.SelectMany(item1 => set, (item1, item2) => new { item1, item2 })
                .Where(group => group.item1 != group.item2)
                .Select(group => new[] {group.item1, group.item2}.AsEnumerable());
        }
    }
}
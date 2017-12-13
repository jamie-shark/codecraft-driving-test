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

            return Enumerable.Range(1, size).Select(x => x.ToString());
        }
    }
}
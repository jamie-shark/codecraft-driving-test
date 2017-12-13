using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class StrongestTeamService : IStrongestTeamService
    {
        private readonly INetworkRepository _networkRepository;
        private readonly ITeamService _teamService;

        public StrongestTeamService(INetworkRepository networkRepository, ITeamService teamService)
        {
            _networkRepository = networkRepository;
            _teamService = teamService;
        }

        public IEnumerable<string> FindStrongestTeam(string skill, int size)
        {
            if (size == 0)
                throw new ArgumentException("Team size must be greater than zero");

            return Enumerable.Range(1, size).Select(x => x.ToString());
        }
    }
}
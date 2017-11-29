using System.Collections.Generic;

namespace ProNet
{
    public class SkillsService : ISkillsService
    {
        private readonly INetworkRepository _networkRepository;

        public SkillsService(INetworkRepository networkRepository)
        {
            _networkRepository = networkRepository;
        }

        public IEnumerable<string> GetSkills(string programmerId)
        {
            return _networkRepository.GetById(programmerId).GetSkills();
        }
    }
}
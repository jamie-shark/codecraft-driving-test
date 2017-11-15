using System.Collections.Generic;

namespace ProNet
{
    public class SkillsService : ISkillsService
    {
        private readonly IGetNetwork _getNetwork;

        public SkillsService(IGetNetwork getNetwork)
        {
            _getNetwork = getNetwork;
        }

        public IEnumerable<string> GetSkills(string programmerId)
        {
            return _getNetwork.GetById(programmerId).GetSkills();
        }
    }
}
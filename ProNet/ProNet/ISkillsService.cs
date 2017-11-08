using System.Collections.Generic;

namespace ProNet
{
    public interface ISkillsService
    {
        IEnumerable<string> GetSkills(string programmerId);
    }
}
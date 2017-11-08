using System.Collections.Generic;

namespace ProNet
{
    public interface ISkillable
    {
        IEnumerable<string> GetSkills();
    }
}
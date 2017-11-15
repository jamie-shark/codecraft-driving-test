using System.Collections.Generic;

namespace ProNet
{
    public interface ISkill
    {
        IEnumerable<string> GetSkills();
    }
}
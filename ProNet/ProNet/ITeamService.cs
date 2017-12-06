using System.Collections.Generic;

namespace ProNet
{
    public interface ITeamService
    {
        double GetStrength(string skill, IEnumerable<string> team);
    }
}
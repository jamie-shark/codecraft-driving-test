using System.Collections.Generic;

namespace ProNet
{
    public interface ITeamStrengthService
    {
        double GetStrength(string skill, IEnumerable<string> team);
    }
}
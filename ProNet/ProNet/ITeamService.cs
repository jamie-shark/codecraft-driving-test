using System.Collections.Generic;

namespace ProNet
{
    public interface ITeamService
    {
        double GetStrength(string skill, IEnumerable<string> team);
        IEnumerable<string> FindStrongestTeam(string skill, int size);
    }
}
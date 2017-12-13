using System.Collections.Generic;

namespace ProNet
{
    public interface IStrongestTeamService
    {
        IEnumerable<string> FindStrongestTeam(string skill, int size);
    }
}
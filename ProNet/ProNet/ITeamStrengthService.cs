using System.Collections.Generic;

namespace ProNet
{
    public interface ITeamStrengthService
    {
        double GetTeamStrength(string skill, IEnumerable<string> team);
        double GetIndividualStrength(string skill, string individual);
    }
}
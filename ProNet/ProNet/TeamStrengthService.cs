using System.Collections.Generic;

namespace ProNet
{
    public class TeamStrengthService
    {
        private readonly ISeparationService _separationService;

        public TeamStrengthService(ISeparationService separationService)
        {
            _separationService = separationService;
        }

        public double GetStrength(string language, IEnumerable<string> team)
        {
            return 0;
        }
    }
}
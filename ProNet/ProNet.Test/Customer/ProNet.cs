namespace ProNet.Test.Customer
{
    public class ProNet : IProNet
    {
        private readonly IRankService _rankService;
        private readonly ISkillsService _skillsService;
        private readonly ISeparationService _separationService;
        private readonly IRecommendationService _recommendationService;
        private readonly ITeamStrengthService _teamStrengthService;

        public ProNet(IRankService rankService, ISkillsService skillsService, ISeparationService separationService, IRecommendationService recommendationService, ITeamStrengthService teamStrengthService)
        {
            _rankService = rankService;
            _skillsService = skillsService;
            _separationService = separationService;
            _recommendationService = recommendationService;
            _teamStrengthService = teamStrengthService;
        }

        public string[] Skills(string programmer)
        {
            return _skillsService.GetSkills(programmer) as string[];
        }

        public string[] Recommendations(string programmer)
        {
            return _recommendationService.GetRecommendations(programmer) as string[];
        }

        public double Rank(string programmer)
        {
            return _rankService.GetRank(programmer);
        }

        public int DegreesOfSeparation(string programmer1, string programmer2)
        {
            return _separationService.GetDegreesBetween(programmer1, programmer2);
        }

        public double TeamStrength(string language, string[] team)
        {
            return _teamStrengthService.GetStrength(language, team);
        }

        public string[] FindStrongestTeam(string language, int teamSize)
        {
            return new string[]{};
        }
    }
}
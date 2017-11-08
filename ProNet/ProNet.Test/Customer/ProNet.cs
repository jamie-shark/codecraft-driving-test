namespace ProNet.Test.Customer
{
    public class ProNet : IProNet
    {
        private readonly IRankService _rankService;
        private readonly ISkillsService _skillsService;

        public ProNet(IRankService rankService, ISkillsService skillsService)
        {
            _rankService = rankService;
            _skillsService = skillsService;
        }

        public string[] Skills(string programmer)
        {
            return (string[]) _skillsService.GetSkills(programmer);
        }

        public string[] Recommendations(string programmer)
        {
            return new string[]{};
        }

        public double Rank(string programmer)
        {
            return _rankService.GetRank(programmer);
        }

        public int DegreesOfSeparation(string programmer1, string programmer2)
        {
            return 0;
        }

        public double TeamStrength(string language, string[] team)
        {
            return 0.0;
        }

        public string[] FindStrongestTeam(string language, int teamSize)
        {
            return new string[]{};
        }
    }
}
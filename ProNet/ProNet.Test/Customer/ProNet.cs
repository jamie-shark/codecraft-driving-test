namespace ProNet.Test.Customer
{
    public class ProNet : IProNet
    {
        private readonly IRankCalculator _rankCalculator;

        public ProNet(IRankCalculator rankCalculator)
        {
            _rankCalculator = rankCalculator;
        }

        public string[] Skills(string programmer)
        {
            return new string[]{};
        }

        public string[] Recommendations(string programmer)
        {
            return new string[]{};
        }

        public double Rank(string programmer)
        {
            return _rankCalculator.GetRank(programmer, 40);
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
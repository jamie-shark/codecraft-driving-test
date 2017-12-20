using NUnit.Framework;
using ProNet.Util;

namespace ProNet.Test.Customer
{
    [TestFixture]
    public class CustomerTestsImpl : AbstractCustomerTests
    {
        protected override IProNet LoadProNet(string filename)
        {
            // load your implementation here
            var fileService = new FileService();
            var networkValidator = new NetworkValidator();
            var networkService = new NetworkRepository(fileService, filename, networkValidator);
            var rankService = new RankService(networkService);
            var skillsService = new SkillsService(networkService);
            var separationService = new SeparationService(networkService);
            var recommendationService = new RecommendationService(networkService);
            var teamStrengthService = new TeamStrengthService(separationService, skillsService, rankService);
            //var permutationService = new PermutationService();
            var permutationService = new CombinationService();
            var strongestTeamService = new StrongestTeamService(networkService, teamStrengthService, permutationService, separationService);

            return new ProNet(rankService, skillsService, separationService, recommendationService, teamStrengthService, strongestTeamService);
        }
    }
}


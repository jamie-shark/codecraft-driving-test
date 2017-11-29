using System.Linq;
using NUnit.Framework;

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

            return new ProNet(rankService, skillsService, separationService, recommendationService);
        }
    }
}


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
            var programmerRepository = new GetNetwork(fileService, filename);
            var rankService = new RankService(programmerRepository);
            var skillsService = new SkillsService(programmerRepository);
            var separationService = new SeparationService(programmerRepository);
            var recommendationService = new RecommendationService(programmerRepository);

            return new ProNet(rankService, skillsService, separationService, recommendationService);
        }
    }
}


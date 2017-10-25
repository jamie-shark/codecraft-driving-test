using NUnit.Framework;

namespace ProNet.Test.Customer
{
    [TestFixture]
    public class CustomerTestsImpl : AbstractCustomerTests
    {
        protected override IProNet LoadProNet(string filename)
        {
            // load your implementation here
            var pageRepository = new XmlProgrammerRepository(filename);
            var pageRankCalculator = new ProgrammerRankCalculator(pageRepository, 40);

            return new ProNet(pageRankCalculator);
        }
    }
}

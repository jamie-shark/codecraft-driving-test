using NUnit.Framework;

namespace ProNet.Test.Customer
{
    [TestFixture]
    public class CustomerTestsImpl : AbstractCustomerTests
    {
        protected override IProNet LoadProNet(string filename)
        {
            // load your implementation here
            var programmerRepository = new XmlProgrammerRepository(filename);
            var programmerRankService = new ProgrammerRankService(programmerRepository);

            return new ProNet(programmerRankService);
        }
    }
}

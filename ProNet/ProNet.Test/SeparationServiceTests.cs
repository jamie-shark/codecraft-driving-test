using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class SeparationServiceTests
    {
        [Test]
        public void Programmers_with_no_connection()
        {
            const int expected = -1;
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            var programmerA = new Programmer("a", null, null);
            programmerRepository.GetById("a").Returns(programmerA);
            var programmerB = new Programmer("b", null, null);
            programmerRepository.GetById("b").Returns(programmerB);
            var separationService = new SeparationService(programmerRepository);
            var degrees = separationService.GetDegreesOfSeparation("a", "b");
            Assert.That(degrees, Is.EqualTo(expected));
        }
    }
}

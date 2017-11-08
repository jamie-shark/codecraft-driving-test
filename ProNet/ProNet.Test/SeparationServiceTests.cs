using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class SeparationServiceTests
    {
        [TestCase("a", "b", -1)]
        [TestCase("a", "a", 0)]
        public void Programmers_with_no_connection(string programmerAId, string programmerBId, int expected)
        {
            var programmerRepository = Substitute.For<IProgrammerRepository>();

            var programmerA = new Programmer(programmerAId, null, null);
            programmerRepository.GetById(programmerAId).Returns(programmerA);

            var programmerB = new Programmer(programmerBId, null, null);
            programmerRepository.GetById(programmerBId).Returns(programmerB);

            var degrees = new SeparationService(programmerRepository).GetDegreesOfSeparation(programmerAId, programmerBId);
            Assert.That(degrees, Is.EqualTo(expected));
        }
    }
}

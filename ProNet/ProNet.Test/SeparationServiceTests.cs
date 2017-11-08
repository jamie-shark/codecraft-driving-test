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
            var programmerRepository = StubProgrammerRepository(
                new Programmer(programmerAId, new string[] { }, null),
                new Programmer(programmerBId, new string[] { }, null));

            AssertDegreesOfSeparation(programmerRepository, programmerAId, programmerBId, expected);
        }

        [Test]
        public void Programmers_with_recommendations_are_1_degree_apart()
        {
            const int expected = 1;
            const string programmerAId = "a";
            const string programmerBId = "b";

            var programmerRepository = StubProgrammerRepository(
                new Programmer(programmerAId, new[] {programmerBId}, null),
                new Programmer(programmerBId, new string[] { }, null));

            AssertDegreesOfSeparation(programmerRepository, programmerAId, programmerBId, expected);
        }

        private static IProgrammerRepository StubProgrammerRepository(IProgrammer programmerA, IProgrammer programmerB)
        {
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetById(programmerA.GetId()).Returns(programmerA);
            programmerRepository.GetById(programmerB.GetId()).Returns(programmerB);
            return programmerRepository;
        }

        private static void AssertDegreesOfSeparation(IProgrammerRepository programmerRepository, string programmerAId, string programmerBId, int expected)
        {
            var degrees = new SeparationService(programmerRepository).GetDegreesOfSeparation(programmerAId, programmerBId);
            Assert.That(degrees, Is.EqualTo(expected));
        }
    }
}

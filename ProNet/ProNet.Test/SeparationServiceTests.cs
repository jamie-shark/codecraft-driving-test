using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class SeparationServiceTests
    {
        private const string ProgrammerAId = "a";
        private const string ProgrammerBId = "b";
        private const string ProgrammerCId = "c";

        [Test]
        public void Separation_with_self()
        {
            var expected = 0;
            var programmerRepository = StubProgrammerRepository(new Programmer(ProgrammerAId, new string[] { }, null));

            var degrees = new SeparationService(programmerRepository).GetDegreesOfSeparation(ProgrammerAId, ProgrammerAId);

            Assert.That(degrees, Is.EqualTo(expected));
        }

        [Test]
        public void Separation_with_no_connection()
        {
            var expected = -1;
            var programmerRepository = StubProgrammerRepository(
                new Programmer(ProgrammerAId, new string[] { }, null),
                new Programmer(ProgrammerBId, new string[] { }, null));
            AssertDegreesOfSeparationBetweenAAndB(programmerRepository, expected);
        }

        [Test]
        public void Separation_with_neighbour()
        {
            var expected = 1;
            var programmerRepository = StubProgrammerRepository(
                new Programmer(ProgrammerAId, new[] {ProgrammerBId}, null),
                new Programmer(ProgrammerBId, new string[] { }, null));
            AssertDegreesOfSeparationBetweenAAndB(programmerRepository, expected);
        }

        [Test]
        public void Separation_with_a_shared_recommendation()
        {
            var expected = 2;
            var programmerRepository = StubProgrammerRepository(
                new Programmer(ProgrammerAId, new[] { ProgrammerCId }, null),
                new Programmer(ProgrammerBId, new[] { ProgrammerCId }, null),
                new Programmer(ProgrammerCId, new string[] { }, null));
            AssertDegreesOfSeparationBetweenAAndB(programmerRepository, expected);
        }

        [Test]
        public void Separation_with_a_shared_recommender()
        {
            var expected = 2;
            var programmerRepository = StubProgrammerRepository(
                new Programmer(ProgrammerAId, new string[] { }, null),
                new Programmer(ProgrammerBId, new string[] { }, null),
                new Programmer(ProgrammerCId, new [] { ProgrammerAId, ProgrammerBId }, null));
            AssertDegreesOfSeparationBetweenAAndB(programmerRepository, expected);
        }

        private static IProgrammerRepository StubProgrammerRepository(params IProgrammer[] programmers)
        {
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            foreach (var programmer in programmers)
                programmerRepository.GetById(programmer.GetId()).Returns(programmer);
            programmerRepository.GetAll().Returns(programmers);
            return programmerRepository;
        }

        private void AssertDegreesOfSeparationBetweenAAndB(IProgrammerRepository programmerRepository, int expected)
        {
            var degrees = new SeparationService(programmerRepository).GetDegreesOfSeparation(ProgrammerAId, ProgrammerBId);
            var degreesWithParametersSwapped = new SeparationService(programmerRepository).GetDegreesOfSeparation(ProgrammerBId, ProgrammerAId);

            Assert.That(degrees, Is.EqualTo(expected));
            Assert.That(degrees, Is.EqualTo(degreesWithParametersSwapped));
        }
    }
}

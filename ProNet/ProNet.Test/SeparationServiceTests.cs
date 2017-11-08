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

        [Test]
        public void Programmers_with_a_shared_recommendation_but_no_direct_relation_are_2_degrees_apart()
        {
            const int expected = 2;
            const string programmerAId = "a";
            const string programmerBId = "b";
            const string programmerCId = "c";

            var programmerRepository = StubProgrammerRepository(
                new Programmer(programmerAId, new[] { programmerCId }, null),
                new Programmer(programmerBId, new[] { programmerCId }, null),
                new Programmer(programmerCId, new string[] { }, null));

            AssertDegreesOfSeparation(programmerRepository, programmerAId, programmerBId, expected);
        }

        [Test]
        public void Programmers_with_a_shared_recommender_but_no_direct_relation_are_2_degrees_apart()
        {
            const int expected = 2;
            const string programmerAId = "a";
            const string programmerBId = "b";
            const string programmerCId = "c";

            var programmerRepository = StubProgrammerRepository(
                new Programmer(programmerAId, new string[] { }, null),
                new Programmer(programmerBId, new string[] { }, null),
                new Programmer(programmerCId, new [] { programmerAId, programmerBId }, null));

            AssertDegreesOfSeparation(programmerRepository, programmerAId, programmerBId, expected);
        }

        private static IProgrammerRepository StubProgrammerRepository(params IProgrammer[] programmers)
        {
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            foreach (var programmer in programmers)
                programmerRepository.GetById(programmer.GetId()).Returns(programmer);
            programmerRepository.GetAll().Returns(programmers);
            return programmerRepository;
        }

        private static void AssertDegreesOfSeparation(IProgrammerRepository programmerRepository, string programmerAId, string programmerBId, int expected)
        {
            var degrees = new SeparationService(programmerRepository).GetDegreesOfSeparation(programmerAId, programmerBId);
            var degreesWithParametersSwapped = new SeparationService(programmerRepository).GetDegreesOfSeparation(programmerBId, programmerAId);

            Assert.That(degrees, Is.EqualTo(expected));
            Assert.That(degrees, Is.EqualTo(degreesWithParametersSwapped));
        }
    }
}

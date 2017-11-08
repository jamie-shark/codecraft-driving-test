using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class ProgrammerRankServiceTests
    {
        [Test]
        public void Programmer_with_no_recommendations()
        {
            var programmers = new List<Programmer> { new Programmer("a", new List<string>()) };

            var expectedResults = new Dictionary<string, double> { { "a", 0.15d } };

            AssertProgrammerRanksAgainstExpectedResults(programmers, expectedResults);
        }

        [Test]
        public void First_worked_example()
        {
            var programmers = new List<Programmer>
            {
                new Programmer("a", new List<string> { "b" }),
                new Programmer("b", new List<string> { "a" })
            };

            var expectedResults = new Dictionary<string, double>
            {
                { "a", 1d },
                { "b", 1d }
            };

            AssertProgrammerRanksAgainstExpectedResults(programmers, expectedResults);
        }

        [Test]
        public void Second_worked_example()
        {
            var programmers = new List<Programmer>
            {
                new Programmer("a", new List<string> { "b", "c" }),
                new Programmer("b", new List<string> { "c" }),
                new Programmer("c", new List<string> { "a" }),
                new Programmer("d", new List<string> { "c" })
            };

            var expectedResults = new Dictionary<string, double>
            {
                { "a", 1.49d },
                { "b", 0.78d },
                { "c", 1.57d },
                { "d", 0.15d }
            };

            AssertProgrammerRanksAgainstExpectedResults(programmers, expectedResults);
        }

        private static void AssertProgrammerRanksAgainstExpectedResults(List<Programmer> programmers, IReadOnlyDictionary<string, double> expectedResults)
        {
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetAll().Returns(programmers);
            programmers.ForEach(programmer => programmerRepository.GetById(programmer.GetId()).Returns(programmer));

            programmers.ForEach(programmer =>
            {
                var programmerId = programmer.GetId();
                var expected = expectedResults[programmerId];

                var rankService = new ProgrammerRankService(programmerRepository);
                var rank = rankService.GetRank(programmerId);
                Assert.That(rank, Is.EqualTo(expected).Within(0.01d));
            });
        }
    }
}

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class RankServiceTests
    {
        [Test]
        public void Programmer_with_no_recommendations()
        {
            var programmers = new List<Programmer> { new Programmer("a", new List<string>(), null) };

            var expectedResults = new Dictionary<string, double> { { "a", 0.15d } };

            AssertRanksAgainstExpectedResults(programmers, expectedResults);
        }

        [Test]
        public void First_worked_example()
        {
            var programmers = new List<Programmer>
            {
                new Programmer("a", new List<string> { "b" }, null),
                new Programmer("b", new List<string> { "a" }, null)
            };

            var expectedResults = new Dictionary<string, double>
            {
                { "a", 1d },
                { "b", 1d }
            };

            AssertRanksAgainstExpectedResults(programmers, expectedResults);
        }

        [Test]
        public void Second_worked_example()
        {
            var programmers = new List<Programmer>
            {
                new Programmer("a", new List<string> { "b", "c" }, null),
                new Programmer("b", new List<string> { "c" }, null),
                new Programmer("c", new List<string> { "a" }, null),
                new Programmer("d", new List<string> { "c" }, null)
            };

            var expectedResults = new Dictionary<string, double>
            {
                { "a", 1.49d },
                { "b", 0.78d },
                { "c", 1.57d },
                { "d", 0.15d }
            };

            AssertRanksAgainstExpectedResults(programmers, expectedResults);
        }

        private static void AssertRanksAgainstExpectedResults(List<Programmer> programmers, IReadOnlyDictionary<string, double> expectedResults)
        {
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetAll().Returns(programmers);
            programmers.ForEach(programmer => programmerRepository.GetById(programmer.GetId()).Returns(programmer));

            programmers.ForEach(programmer =>
            {
                var programmerId = programmer.GetId();
                var expected = expectedResults[programmerId];

                var rankService = new RankService(programmerRepository);
                var rank = rankService.GetRank(programmerId);
                Assert.That(rank, Is.EqualTo(expected).Within(0.01d));
            });
        }
    }
}

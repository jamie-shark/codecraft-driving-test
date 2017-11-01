using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class ProgrammerRankCalculatorTests
    {
        [Test]
        public void Programmer_with_no_recommendations()
        {
            const string idA = "a";

            var programmers = new List<Programmer> { new Programmer(idA, new List<string>()) };

            var expectedResults = new Dictionary<string, double> { { idA, 0.15d } };

            AssertProgrammerRanksAgainstExpectedResults(programmers, expectedResults);
        }

        [Test]
        public void First_worked_example()
        {
            const string idA = "a";
            const string idB = "b";

            var programmers = new List<Programmer>
            {
                new Programmer(idA, new List<string> { idB }),
                new Programmer(idB, new List<string> { idA })
            };

            var expectedResults = new Dictionary<string, double>
            {
                { idA, 1d },
                { idB, 1d }
            };

            AssertProgrammerRanksAgainstExpectedResults(programmers, expectedResults);
        }

        [Test]
        public void Second_worked_example()
        {

            const string idA = "a";
            const string idB = "b";
            const string idC = "c";
            const string idD = "d";

            var programmers = new List<Programmer>
            {
                new Programmer(idA, new List<string> { idB, idC }),
                new Programmer(idB, new List<string> { idC }),
                new Programmer(idC, new List<string> { idA }),
                new Programmer(idD, new List<string> { idC })
            };

            var expectedResults = new Dictionary<string, double>
            {
                { idA, 1.49d },
                { idB, 0.78d },
                { idC, 1.57d },
                { idD, 0.15d }
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

                var rankCalculator = new ProgrammerRankCalculator(programmerRepository);
                var rank = rankCalculator.GetRank(programmerId);
                Assert.That(rank, Is.EqualTo(expected).Within(0.01d));
            });
        }
    }
}

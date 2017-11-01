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
            const string programmerId = "a";
            const double expected = 0.15d;
            const double range = 0.01d;

            var programmer = new Programmer(programmerId, new List<string>());
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetById(programmerId).Returns(programmer);
            programmerRepository.GetAll().Returns(new List<Programmer> {programmer});
            var rankCalculator = new ProgrammerRankCalculator(programmerRepository);

            var rank = rankCalculator.GetRank(programmerId);

            Assert.That(rank, Is.EqualTo(expected).Within(range));
        }

        [Test]
        public void First_worked_example()
        {
            const string idA = "a";
            const string idB = "b";
            const double expected = 1.0d;
            const double range = 0.01d;

            var programmers = new List<Programmer>
            {
                new Programmer(idA, new List<string> { idB }),
                new Programmer(idB, new List<string> { idA })
            };

            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetAll().Returns(programmers);
            programmers.ForEach(programmer => programmerRepository.GetById(programmer.GetId()).Returns(programmer));

            programmers.ForEach(programmer =>
            {
                var rankCalculator = new ProgrammerRankCalculator(programmerRepository);
                var rank = rankCalculator.GetRank(programmer.GetId());
                Assert.That(rank, Is.EqualTo(expected).Within(range));
            });
        }

        [Test]
        public void Second_worked_example()
        {

            const string idA = "a";
            const string idB = "b";
            const string idC = "c";
            const string idD = "d";
            var expectedResults = new Dictionary<string, double>
            {
                { idA, 1.49d },
                { idB, 0.78d },
                { idC, 1.57d },
                { idD, 0.15d }
            };
            const double range = 0.01d;

            var programmers = new List<Programmer>
            {
                new Programmer(idA, new List<string> { idB, idC }),
                new Programmer(idB, new List<string> { idC }),
                new Programmer(idC, new List<string> { idA }),
                new Programmer(idD, new List<string> { idC })
            };

            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetAll().Returns(programmers);
            programmers.ForEach(programmer => programmerRepository.GetById(programmer.GetId()).Returns(programmer));

            programmers.ForEach(programmer =>
            {
                var rankCalculator = new ProgrammerRankCalculator(programmerRepository);
                var rank = rankCalculator.GetRank(programmer.GetId());
                Assert.That(rank, Is.EqualTo(expectedResults[programmer.GetId()]).Within(range));
            });
        }
    }
}

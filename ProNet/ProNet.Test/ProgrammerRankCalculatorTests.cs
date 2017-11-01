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
            const int settleLimit = 40;
            const double expected = 0.15d;
            const double range = 0.01d;

            var programmer = new Programmer(programmerId, new List<string>());
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetById(programmerId).Returns(programmer);
            programmerRepository.GetAll().Returns(new List<Programmer> {programmer});
            var rankCalculator = new ProgrammerRankCalculator(programmerRepository);

            var rank = rankCalculator.GetRank(programmerId, settleLimit);

            Assert.That(rank, Is.EqualTo(expected).Within(range));
        }

        [Test]
        public void Two_programmers_recommending_each_other()
        {
            const string idA = "a";
            const string idB = "b";
            const int settleLimit = 40;
            const double expected = 1.0d;
            const double range = 0.01d;

            var programmerA = new Programmer(idA, new List<string> { idB });
            var programmerB = new Programmer(idB, new List<string> { idA });
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetById(idA).Returns(programmerA);
            programmerRepository.GetById(idB).Returns(programmerB);
            programmerRepository.GetAll().Returns(new List<Programmer> { programmerA, programmerB });
            var rankCalculator = new ProgrammerRankCalculator(programmerRepository);

            var rank = rankCalculator.GetRank(idA, settleLimit);

            Assert.That(rank, Is.EqualTo(expected).Within(range));
        }
    }
}

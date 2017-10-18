using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class RankCalculatorTests
    {
        [Test]
        public void Programmer_with_no_recommendations()
        {
            var programmer = new Programmer("a", new List<string>());
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetAll().Returns(new List<Programmer> {programmer});

            var rankCalculator = new RankCalculator(programmerRepository, 40);

            var rank = rankCalculator.Calculate(programmer);

            Assert.That(rank, Is.EqualTo(0.15m));
        }

        [Test]
        public void Two_programmers_recommending_each_other()
        {
            var programmer1 = new Programmer("a", new List<string> { "b" });
            var programmer2 = new Programmer("b", new List<string> { "a" });
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetAll().Returns(new List<Programmer> { programmer1, programmer2 });

            var rankCalculator = new RankCalculator(programmerRepository, 40);

            var rank = rankCalculator.Calculate(programmer1);

            Assert.That(rank, Is.EqualTo(1.0m).Within(0.0001m));
        }
    }
}

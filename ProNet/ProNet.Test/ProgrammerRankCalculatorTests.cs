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
            var programmer = new Programmer("a", new List<string>());
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetById("a").Returns(programmer);
            programmerRepository.GetAll().Returns(new List<Programmer> {programmer});
            var rankCalculator = new ProgrammerRankCalculator(programmerRepository);

            var rank = rankCalculator.GetRank("a", 40);

            Assert.That(rank, Is.EqualTo(0.15d).Within(0.01d));
        }

        [Test]
        public void Two_programmers_recommending_each_other()
        {
            var programmerA = new Programmer("a", new List<string> { "b" });
            var programmerB = new Programmer("b", new List<string> { "a" });
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetById("a").Returns(programmerA);
            programmerRepository.GetById("b").Returns(programmerB);
            programmerRepository.GetAll().Returns(new List<Programmer> { programmerA, programmerB });
            var rankCalculator = new ProgrammerRankCalculator(programmerRepository);

            var rank = rankCalculator.GetRank("a", 40);

            Assert.That(rank, Is.EqualTo(1.0d).Within(0.01d));
        }
    }
}

using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class PageRankCalculatorTests
    {
        [Test]
        public void Page_with_no_recommendations()
        {
            var page = new Programmer("a", new List<string>());
            var pageRepository = Substitute.For<IPageRepository>();
            pageRepository.GetAll().Returns(new List<Programmer> {page});

            var pageRankCalculator = new PageRankCalculator(pageRepository, 40);

            var pageRank = pageRankCalculator.PageRank(page);

            Assert.That(pageRank, Is.EqualTo(0.15m));
        }

        [Test]
        public void Two_pages_recommending_each_other()
        {
            var pageA = new Programmer("a", new List<string> { "b" });
            var pageB = new Programmer("b", new List<string> { "a" });
            var pageRepository = Substitute.For<IPageRepository>();
            pageRepository.GetAll().Returns(new List<Programmer> { pageA, pageB });

            var rankCalculator = new PageRankCalculator(pageRepository, 40);

            var pageRank = rankCalculator.PageRank(pageA);

            Assert.That(pageRank, Is.EqualTo(1.0m).Within(0.01m));
        }
    }
}

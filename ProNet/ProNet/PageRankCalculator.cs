using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class PageRankCalculator
    {
        private readonly IPageRepository _pageRepository;
        private readonly int _settleLimit;

        private int _iteration;

        public PageRankCalculator(IPageRepository pageRepository, int settleLimit)
        {
            _pageRepository = pageRepository;
            _settleLimit = settleLimit;
        }

        public decimal PageRank(IPageRankable page)
        {
            const decimal dampingFactor = 0.85m;
            var rank = 0m;

            while (++_iteration < _settleLimit)
                rank = (1 - dampingFactor) + dampingFactor * OthersThatReference(page)
                            .Select(p => PageRank(p) / ReferenceCount(p))
                            .Sum();

            return rank;
        }

        private IEnumerable<IPageRankable> OthersThatReference(IPageRankable page)
        {
            return _pageRepository
                .GetAll()
                .Where(p => p.Recommendations.Contains(page.Id));
        }

        private static int ReferenceCount(IPageRankable page)
        {
            return page.Recommendations.Count();
        }
    }
}
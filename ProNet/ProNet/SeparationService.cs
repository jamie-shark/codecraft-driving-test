using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class SeparationService
    {
        private readonly IGetNetwork _programmers;

        public SeparationService(IGetNetwork getNetwork)
        {
            _programmers = getNetwork;
        }

        public int GetDegreesBetween(string programmerAId, string programmerBId)
        {
            if (programmerAId == programmerBId)
                return -1;

            var programmerA = _programmers.GetById(programmerAId);
            var programmerB = _programmers.GetById(programmerBId);

            if (AreDirectlyRelated(programmerA, programmerB))
                return 0;

            if (AreCloselyRelated(programmerA, programmerB))
                return 1;

            return -1;
        }

        private static bool AreDirectlyRelated(IRecommend programmerA, IRecommend programmerB)
        {
            return programmerA
                   .GetRecommendations()
                   .Contains(programmerB.GetId())
                || programmerB
                   .GetRecommendations()
                   .Contains(programmerA.GetId());
        }

        private bool AreCloselyRelated(IProgrammer programmerA, IProgrammer programmerB)
        {
            return SharedRecommends(programmerA, programmerB).Any()
                || SharedRecommenders(programmerA, programmerB).Any()
                || SecondRecommends(programmerA).Contains(programmerB.GetId())
                || SecondRecommends(programmerB).Contains(programmerA.GetId());
        }

        private static IEnumerable<string> SharedRecommends(IRecommend programmerA, IRecommend programmerB)
        {
            return programmerA
                .GetRecommendations()
                .Intersect(programmerB.GetRecommendations());
        }

        private IEnumerable<IRecommend> SharedRecommenders(IRecommended programmerA, IRecommended programmerB)
        {
            return programmerA
                .GetRecommenders(_programmers.GetAll())
                .Intersect(programmerB.GetRecommenders(_programmers.GetAll()));
        }

        private IEnumerable<string> SecondRecommends(IRecommend programmer)
        {
            return programmer
                .GetRecommendations()
                .Select(id => _programmers.GetById(id))
                .SelectMany(recommendation => recommendation.GetRecommendations())
                .Distinct();
        }
    }
}
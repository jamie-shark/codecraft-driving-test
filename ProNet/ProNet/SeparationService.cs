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

            if (programmerA.GetRecommendations().Contains(programmerBId) || programmerB.GetRecommendations().Contains(programmerAId))
                return 0;

            if (programmerA.GetRecommendations().Intersect(programmerB.GetRecommendations()).Any())
                return 1;

            if (programmerA.GetRecommenders(_programmers.GetAll()).Intersect(programmerB.GetRecommenders(_programmers.GetAll())).Any())
                return 1;

            var aSecondDegree =
                programmerA
                    .GetRecommendations()
                    .Select(id => _programmers.GetById(id))
                    .SelectMany(recommendation => recommendation.GetRecommendations())
                    .Distinct();

            var bSecondDegree =
                programmerB
                    .GetRecommendations()
                    .Select(id => _programmers.GetById(id))
                    .SelectMany(recommendation => recommendation.GetRecommendations())
                    .Distinct();

            if (aSecondDegree.Contains(programmerBId) || bSecondDegree.Contains(programmerAId))
                return 1;

            return -1;
        }
    }
}
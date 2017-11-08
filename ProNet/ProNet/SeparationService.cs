using System.Linq;

namespace ProNet
{
    public class SeparationService
    {
        private readonly IProgrammerRepository _programmerRepository;

        public SeparationService(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public int GetDegreesOfSeparation(string programmerAId, string programmerBId)
        {
            if (programmerAId == programmerBId)
                return 0;

            var programmerA = _programmerRepository.GetById(programmerAId);
            var programmerB = _programmerRepository.GetById(programmerBId);

            if (programmerA.GetRecommendations().Contains(programmerBId) || programmerB.GetRecommendations().Contains(programmerAId))
                return 1;

            return -1;
        }
    }
}
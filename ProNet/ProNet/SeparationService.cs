namespace ProNet
{
    public class SeparationService
    {
        private readonly IProgrammerRepository _programmerRepository;

        public SeparationService(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public int GetDegreesOfSeparation(string programmerA, string programmerB)
        {
            return -1;
        }
    }
}
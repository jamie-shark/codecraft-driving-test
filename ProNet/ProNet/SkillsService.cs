namespace ProNet
{
    public class SkillsService
    {
        private readonly IProgrammerRepository _programmerRepository;

        public SkillsService(IProgrammerRepository programmerRepository)
        {
            _programmerRepository = programmerRepository;
        }

        public string[] GetSkills(string programmerId)
        {
            return new[] {"a"};
        }
    }
}
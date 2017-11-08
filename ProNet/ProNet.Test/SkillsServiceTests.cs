using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class SkillsServiceTests
    {
        [Test]
        public void GetSkills_calls_ProgrammerRepository_with_given_id()
        {
            var programmerId = "programmer";
            var programmerRepository = Substitute.For<IProgrammerRepository>();
            programmerRepository.GetById(programmerId).Returns(new Programmer(programmerId, null, new[] {"a"}));
            var skillsService = new SkillsService(programmerRepository);
            var skills = skillsService.GetSkills(programmerId);
            Assert.That(skills, Is.EqualTo(new[] {"a"}));
        }
    }
}

using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class SkillsServiceTests
    {
        [TestCase("a")]
        [TestCase("b")]
        public void GetSkills_calls_ProgrammerRepository_with_given_id(string expectedSkills)
        {
            var id = "programmer id";
            var programmerRepository = Substitute.For<IGetNetwork>();
            programmerRepository.GetById(id).Returns(new Programmer(id, null, new[] {expectedSkills}));
            var skillsService = new SkillsService(programmerRepository);
            var skills = skillsService.GetSkills(id);
            Assert.That(skills, Is.EqualTo(new[] { expectedSkills }));
        }
    }
}

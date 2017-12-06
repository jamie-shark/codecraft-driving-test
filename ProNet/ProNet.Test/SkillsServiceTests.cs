using System.Collections.Generic;
using System.Linq;
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
            var programmerRepository = Substitute.For<INetworkRepository>();
            programmerRepository.GetById(id).Returns(new Programmer(id, null, new[] {expectedSkills}));
            var skillsService = new SkillsService(programmerRepository);
            var skills = skillsService.GetSkills(id);
            Assert.That(skills, Is.EqualTo(new[] { expectedSkills }));
        }

        [Test]
        public void GetSkillRank_returns_zero_if_programmer_does_not_have_given_skill()
        {
            var id = "id";
            var networkRepository = Substitute.For<INetworkRepository>();
            networkRepository.GetById(id).Returns(new Programmer(id, null, new [] { "other skill" }));
            var skillsService = new SkillsService(networkRepository);
            var skillRank = skillsService.GetSkillIndex(id, "skill");
            Assert.That(skillRank, Is.EqualTo(0));
        }

        [Test]
        public void GetSkillIndex_returns_zero_if_programmer_has_no_skills()
        {
            var id = "id";
            var networkRepository = Substitute.For<INetworkRepository>();
            networkRepository.GetById(id).Returns(new Programmer(id, null, new string[] { }));
            var skillsService = new SkillsService(networkRepository);
            var skillIndex = skillsService.GetSkillIndex(id, "skill");
            Assert.That(skillIndex, Is.EqualTo(0));
        }

        [Test]
        public void GetSkillIndex_returns_one_based_index_of_given_skill()
        {
            var id = "id";
            var networkRepository = Substitute.For<INetworkRepository>();
            networkRepository.GetById(id).Returns(new Programmer(id, null, new[] { "other skill", "skill" }));
            var skillsService = new SkillsService(networkRepository);
            var skillIndex = skillsService.GetSkillIndex(id, "skill");
            Assert.That(skillIndex, Is.EqualTo(2));
        }
    }
}

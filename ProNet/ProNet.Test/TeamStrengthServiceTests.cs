using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class TeamStrengthServiceTests
    {
        private ISeparationService _separationService;
        private ISkillsService _skillsService;
        private IRankService _rankService;
        private TeamStrengthService _teamStrengthService;
        private string _skill;
        private IEnumerable<string> _team;
        private decimal _expectedStrength;

        [SetUp]
        public void SetUp()
        {
            _separationService = Substitute.For<ISeparationService>();
            _skillsService = Substitute.For<ISkillsService>();
            _rankService = Substitute.For<IRankService>();
            _teamStrengthService = new TeamStrengthService(_separationService, _skillsService, _rankService);

            _skill = "";
            _team = new List<string> { "leader", "a", "b" };
        }

        [TearDown]
        public void TearDown()
        {
            var strength = _teamStrengthService.GetStrength(_skill, _team);
            Assert.That(strength, Is.EqualTo(_expectedStrength));
        }

        [TestCase(new string[] {}, 0)]
        [TestCase(new[] { "leader" }, 0)]
        public void Strength_of_empty_team_is_zero(IEnumerable<string> team, decimal expected)
        {
            _team = team;
            _expectedStrength = expected;
        }

        [Test]
        public void Strength_of_team_with_no_connection_is_zero()
        {
            _separationService.GetDegreesBetween(Arg.Any<string>(), Arg.Any<string>()).Returns(0);
            _expectedStrength = 0;
        }

        [Test]
        public void Strength_of_team_with_member_that_does_not_know_chosen_skill_is_zero()
        {
            _skill = "skill";
            _skillsService.GetSkills("leader").Returns(new [] { "skill" });
            _skillsService.GetSkills("a").Returns(new[] { "NOT skill" });
            _skillsService.GetSkills("b").Returns(new[] { "skill" });
            _expectedStrength = 0;
        }

        [TestCase(1)]
        [TestCase(5)]
        public void Strength_of_team_with_one_for_skill_index_and_degrees_of_separation_is_average_of_page_ranks(int averageRank)
        {
            _skillsService.GetSkillIndex(Arg.Any<string>(), Arg.Any<string>()).Returns(1);
            _separationService.GetDegreesBetween(Arg.Any<string>(), Arg.Any<string>()).Returns(1);
            _rankService.GetRank(Arg.Any<string>()).Returns(averageRank);
            _expectedStrength = averageRank;
        }
    }
}

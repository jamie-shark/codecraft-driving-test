using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class StrongestTeamServiceTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public void Strongest_team_is_of_specified_size(int expected)
        {
            var networkRepository = Substitute.For<INetworkRepository>();
            var skills = new [] {"skill"};
            networkRepository.GetAll().Returns(new[]
            {
                new Programmer("a", null, skills),
                new Programmer("b", null, skills),
                new Programmer("c", null, skills)
            });
            var teamService = Substitute.For<ITeamStrengthService>();
            var team = new StrongestTeamService(networkRepository, teamService).FindStrongestTeam("skill", expected);

            Assert.That(team.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void Strongest_team_has_the_highest_possible_team_strength_for_given_skill_and_size()
        {
            const string skill = "valid skill";
            const string programmerA = "a";
            const string programmerB = "b";
            const string programmerC = "c";

            var networkRepository = Substitute.For<INetworkRepository>();
            networkRepository.GetAll().Returns(new[]
            {
                new Programmer(programmerA, null, null),
                new Programmer(programmerB, null, null),
                new Programmer(programmerC, null, null)
            });

            var teamService = Substitute.For<ITeamStrengthService>();
            teamService.GetStrength(skill, new[] { programmerA, programmerB }).Returns(0d);
            teamService.GetStrength(skill, new[] { programmerC, programmerB }).Returns(1d);
            teamService.GetStrength(skill, new[] { programmerA, programmerC }).Returns(2d);

            var strongestTeam = new StrongestTeamService(networkRepository, teamService).FindStrongestTeam(skill, 2);

            Assert.That(strongestTeam, Is.EqualTo(new[] { programmerA, programmerC }));
        }

        [Test]
        public void Strongest_team_must_have_at_least_one_member()
        {
            Assert.Throws<ArgumentException>(() => new StrongestTeamService(Substitute.For<INetworkRepository>(), Substitute.For<ITeamStrengthService>()).FindStrongestTeam("valid skill", 0));
        }
    }
}

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
        public void Strongest_team_must_have_at_least_one_member()
        {
            Assert.Throws<ArgumentException>(() => new StrongestTeamService(Substitute.For<INetworkRepository>(), Substitute.For<ITeamStrengthService>()).FindStrongestTeam("valid skill", 0));
        }
    }
}

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
        [TestCase(3)]
        public void Strongest_team_is_of_specified_size(int expected)
        {
            var networkRepository = Substitute.For<INetworkRepository>();
            networkRepository.GetAll().Returns(new[]
            {
                new Programmer(null, null, null),
                new Programmer(null, null, null),
                new Programmer(null, null, null)
            });
            var teamService = Substitute.For<ITeamStrengthService>();
            var team = new StrongestTeamService(networkRepository, teamService).FindStrongestTeam("", expected);
            Assert.That(team.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void Strongest_team_must_have_at_least_one_member()
        {
            Assert.Throws<ArgumentException>(() => new StrongestTeamService(Substitute.For<INetworkRepository>(), Substitute.For<ITeamStrengthService>()).FindStrongestTeam("valid skill", 0));
        }
    }
}

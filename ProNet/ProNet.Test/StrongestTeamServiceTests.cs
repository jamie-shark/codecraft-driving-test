using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using ProNet.Util;

namespace ProNet.Test
{
    [TestFixture]
    public class StrongestTeamServiceTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Strongest_team_is_of_specified_size(int expected)
        {
            var networkRepository = Substitute.For<INetworkRepository>();
            var skills = new [] {"skill"};
            networkRepository.GetAll().Returns(new[]
            {
                new Programmer("a", null, skills),
                new Programmer("b", null, skills),
                new Programmer("c", null, skills),
                new Programmer("d", null, skills),
                new Programmer("e", null, skills)
            });
            var teamService = Substitute.For<ITeamStrengthService>();

            var team = new StrongestTeamService(networkRepository, teamService, new PermutationService()).FindStrongestTeam("skill", expected);

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

            var team1 = new[] { programmerA, programmerB };
            var team2 = new[] { programmerC, programmerB };
            var team3 = new[] { programmerA, programmerC };

            var teamService = Substitute.For<ITeamStrengthService>();
            teamService.GetStrength(skill, team1).Returns(0d);
            teamService.GetStrength(skill, team2).Returns(1d);
            teamService.GetStrength(skill, team3).Returns(2d);

            var permutationService = Substitute.For<IPermutationService>();
            permutationService.GetPermutations(new [] {programmerA, programmerB, programmerC}, 2).Returns(new [] {team1, team2, team3});

            var strongestTeam = new StrongestTeamService(networkRepository, teamService, permutationService).FindStrongestTeam(skill, 2);

            Assert.That(strongestTeam, Is.EqualTo(team3));
        }

        [Test]
        public void Strongest_team_must_have_at_least_one_member()
        {
            Assert.Throws<ArgumentException>(() =>
                new StrongestTeamService(Substitute.For<INetworkRepository>(), Substitute.For<ITeamStrengthService>(), Substitute.For<IPermutationService>())
                    .FindStrongestTeam("valid skill", 0));
        }
    }
}

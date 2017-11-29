using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class TeamStrengthServiceTests
    {
        [TestCase(new string[] {}, 0)]
        [TestCase(new[] { "leader" }, 0)]
        public void Strength_of_empty_team_is_zero(IEnumerable<string> team, decimal expected)
        {
            var strength = new TeamStrengthService(null).GetStrength("", team);
            Assert.That(strength, Is.EqualTo(expected));
        }

        [Test]
        public void Strength_of_team_with_no_connection_is_zero()
        {
            var team = new List<string> {"leader", "a", "b"};
            var separationService = Substitute.For<ISeparationService>();
            separationService.GetDegreesBetween(Arg.Any<string>(), Arg.Any<string>()).Returns(0);
            var strength = new TeamStrengthService(separationService).GetStrength("", team);
            Assert.That(strength, Is.EqualTo(0));
        }
    }
}

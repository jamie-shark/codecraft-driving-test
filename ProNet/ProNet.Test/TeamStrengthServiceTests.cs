using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class TeamStrengthServiceTests
    {
        [Test]
        public void Strength_of_empty_team_is_zero()
        {
            var language = "";
            var team = new string[] {};
            var strength = new TeamStrengthService().GetStrength(language, team);
            Assert.That(strength, Is.EqualTo(0));
        }

        [Test]
        public void Strength_of_leader_only_team_is_zero()
        {
            var language = "";
            var team = new [] { "leader" };
            var strength = new TeamStrengthService().GetStrength(language, team);
            Assert.That(strength, Is.EqualTo(0));
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class TeamStrengthServiceTests
    {
        [TestCase("", new string[] {}, 0)]
        [TestCase("", new[] { "leader" }, 0)]
        public void Strength_of_empty_team_is_zero(string language, IEnumerable<string> team, decimal expected)
        {
            var strength = new TeamStrengthService().GetStrength(language, team);
            Assert.That(strength, Is.EqualTo(expected));
        }
    }
}

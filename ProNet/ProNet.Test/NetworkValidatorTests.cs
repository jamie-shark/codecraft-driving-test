using System;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class NetworkValidatorTests
    {
        private Network _network;

        [SetUp]
        public void SetUp()
        {
            _network = new Network();
        }

        [TearDown]
        public void TearDown()
        {
            Assert.Throws<ArgumentException>(() => new NetworkValidator().Validate(_network));
        }

        [Test]
        public void Throws_Argument_Exception_when_network_has_no_list_of_programmers()
        {
        }

        [Test]
        public void Throws_Argument_Exception_when_network_has_programmer_with_no_name()
        {
            _network.Programmer = new[] { new NetworkProgrammer { name = null } };
        }

        [Test]
        public void Throws_Argument_Exception_when_network_has_programmer_with_no_list_of_recommendations()
        {
            _network.Programmer = new[] { new NetworkProgrammer { name = "some", Recommendations = null } };
        }
    }
}

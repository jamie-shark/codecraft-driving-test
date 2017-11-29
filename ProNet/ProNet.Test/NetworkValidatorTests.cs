using System;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class NetworkValidatorTests
    {
        [Test]
        public void Throws_Argument_Exception_when_network_has_no_list_of_programmers()
        {
            var network = new Network();
            Assert.Throws<ArgumentException>(() => new NetworkValidator().Validate(network));
        }

        [Test]
        public void Throws_Argument_Exception_when_network_has_programmer_with_no_name()
        {
            var network = new Network();
            network.Programmer = new[] { new NetworkProgrammer { name = null } };
            Assert.Throws<ArgumentException>(() => new NetworkValidator().Validate(network));
        }
    }
}

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
    }
}

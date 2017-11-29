using System;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class FileServiceTests
    {
        [Test]
        public void Throws_ArgumentException_given_a_file_that_does_not_exist()
        {
            Assert.Throws<ArgumentException>(() => new FileService().GetContents("Non-existant file"));
        }
    }
}

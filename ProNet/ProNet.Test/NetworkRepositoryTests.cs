using System;
using System.IO;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class NetworkRepositoryTests
    {
        [Test]
        public void Throws_Argument_Exception_when_id_not_found()
        {
            var fileService = Substitute.For<IFileService>();
            const string contents = @"<Network>
                                        <Programmer name='valid'>
                                          <Recommendations>
                                          </Recommendations>
                                          <Skills>
                                          </Skills>
                                        </Programmer>
                                      </Network>";
            fileService.GetContents("file").Returns(new MemoryStream(Encoding.ASCII.GetBytes(contents)));
            Assert.Throws<ArgumentException>(() => new NetworkRepository(fileService, "file").GetById("invalid"));
        }

        [Test]
        public void Throws_Argument_Exception_when_file_cannot_be_parsed_as_network()
        {
            var fileService = Substitute.For<IFileService>();
            const string contents = "invalid network";
            fileService.GetContents("file").Returns(new MemoryStream(Encoding.ASCII.GetBytes(contents)));
            Assert.Throws<ArgumentException>(() => new NetworkRepository(fileService, "file").GetAll());
        }
    }
}

﻿using System;
using System.IO;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class XmlProgrammerRepositoryTests
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
            fileService.GetContents().Returns(new MemoryStream(Encoding.ASCII.GetBytes(contents)));
            Assert.Throws<ArgumentException>(() => new XmlProgrammerRepository(fileService).GetById("invalid"));
        }
    }
}

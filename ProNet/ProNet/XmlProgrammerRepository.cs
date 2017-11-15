using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace ProNet
{
    //TODO: This was spiked and needs to be test driven
    public class XmlProgrammerRepository : IProgrammerRepository
    {
        private readonly IFileService _fileService;
        private readonly string _networkFilePath;

        public XmlProgrammerRepository(IFileService fileService, string networkFilePath)
        {
            _fileService = fileService;
            _networkFilePath = networkFilePath;
        }

        public IEnumerable<IProgrammer> GetAll()
        {
            Network network;

            try
            {
                var serilaizer = new XmlSerializer(typeof(Network));
                network = serilaizer.Deserialize(_fileService.GetContents(_networkFilePath)) as Network;
            }
            catch (InvalidOperationException e)
            {
                throw new ArgumentException($"File {_networkFilePath} is not a valid ProNet data file");
            }

            return network?.Programmer.Select(p => new Programmer(p.name, p.Recommendations, p.Skills));
        }

        public IProgrammer GetById(string id)
        {
            var programmer = GetAll().FirstOrDefault(r => r.GetId() == id);
            if (programmer == null) throw new ArgumentException($"Programmer {id} was not found");
            return programmer;
        }
    }
}
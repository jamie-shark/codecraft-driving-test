using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ProNet
{
    //TODO: This was spiked and needs to be test driven
    public class XmlProgrammerRepository : IProgrammerRepository
    {
        private readonly IFileService _fileService;

        public XmlProgrammerRepository(IFileService fileService)
        {
            _fileService = fileService;
        }

        public IEnumerable<IProgrammer> GetAll()
        {
            var serilaizer = new XmlSerializer(typeof(Network));
            var network = serilaizer.Deserialize(_fileService.GetContents()) as Network;

            if (network == null) throw new FileLoadException($"{_fileService} could not be parsed as a Network");

            return network.Programmer.Select(p => new Programmer(p.name, p.Recommendations, p.Skills));
        }

        public IProgrammer GetById(string id)
        {
            var programmer = GetAll().FirstOrDefault(r => r.GetId() == id);
            if (programmer == null) throw new ArgumentException($"Programmer {id} was not found");
            return programmer;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ProNet
{
    public class NetworkRepository : INetworkRepository
    {
        private readonly IFileService _fileService;
        private readonly string _networkFilePath;
        private readonly INetworkValidator _networkValidator;

        public NetworkRepository(IFileService fileService, string networkFilePath, INetworkValidator networkValidator)
        {
            _fileService = fileService;
            _networkFilePath = networkFilePath;
            _networkValidator = networkValidator;
        }

        public IEnumerable<IProgrammer> GetAll()
        {
            Network network;
            TryGetNetwork(out network);

            try
            {
                _networkValidator.Validate(network);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"File {_networkFilePath} is not a valid ProNet data file");
            }

            return network.Programmer.Select(p => new Programmer(p.name, p.Recommendations, p.Skills));
        }

        private void TryGetNetwork(out Network network)
        {
            try
            {
                var serilaizer = new XmlSerializer(typeof(Network));
                network = serilaizer.Deserialize(_fileService.GetContents(_networkFilePath)) as Network;
            }
            catch (InvalidOperationException e)
            {
                throw new ArgumentException($"File {_networkFilePath} is not a valid ProNet data file");
            }
        }

        public IProgrammer GetById(string id)
        {
            var programmer = GetAll().FirstOrDefault(r => r.GetId() == id);
            if (programmer == null) throw new ArgumentException($"Programmer {id} was not found");
            return programmer;
        }
    }
}
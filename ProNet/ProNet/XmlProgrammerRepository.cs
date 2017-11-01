using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ProNet
{
    //TODO: This was spiked and needs to be test driven
    public class XmlProgrammerRepository : IProgrammerRepository
    {
        private readonly string _filename;

        public XmlProgrammerRepository(string filename)
        {
            _filename = filename;
        }

        public IEnumerable<IRankable> GetAll()
        {
            var serilaizer = new XmlSerializer(typeof(Network));
            var network = serilaizer.Deserialize(new FileStream(_filename, FileMode.Open, FileAccess.Read)) as Network;

            if (network == null) throw new FileLoadException($"{_filename} could not be parsed as a Network");

            return network.Programmer.Select(p => new Programmer(p.name, p.Recommendations));
        }

        public IRankable GetById(string id)
        {
            return GetAll().First(r => r.GetId() == id);
        }
    }
}
using System;
using System.Collections.Generic;

namespace ProNet.Test.Customer
{
    public class XmlProgrammerRepository : IProgrammerRepository
    {
        private readonly string _filename;

        public XmlProgrammerRepository(string filename)
        {
            _filename = filename;
        }

        public IEnumerable<IRankable> GetAll()
        {
            throw new NotImplementedException();
        }

        public IRankable GetById(string pageId)
        {
            throw new NotImplementedException();
        }
    }
}
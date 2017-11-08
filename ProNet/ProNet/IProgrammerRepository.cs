using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammerRepository
    {
        IEnumerable<IProgrammer> GetAll();
        IProgrammer GetById(string id);
    }
}
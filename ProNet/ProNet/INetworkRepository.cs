using System.Collections.Generic;

namespace ProNet
{
    public interface INetworkRepository
    {
        IEnumerable<IProgrammer> GetAll();
        IProgrammer GetById(string id);
    }
}
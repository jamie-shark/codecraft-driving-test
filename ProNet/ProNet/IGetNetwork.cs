using System.Collections.Generic;

namespace ProNet
{
    public interface IGetNetwork
    {
        IEnumerable<IProgrammer> GetAll();
        IProgrammer GetById(string id);
    }
}
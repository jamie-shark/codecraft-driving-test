using System.Collections.Generic;

namespace ProNet
{
    public interface IGetProgrammers
    {
        IEnumerable<IProgrammer> GetAll();
        IProgrammer GetById(string id);
    }
}
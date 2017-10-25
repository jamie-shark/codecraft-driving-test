using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammerRepository
    {
        IEnumerable<IRankable> GetAll();
        IRankable GetById(string pageId);
    }
}
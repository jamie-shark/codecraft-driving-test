using System.Collections.Generic;

namespace ProNet
{
    public interface IPageRepository
    {
        IEnumerable<IPageRankable> GetAll();
    }
}
using System.Collections.Generic;

namespace ProNet
{
    public interface IRecommended : IRecommend
    {
        IEnumerable<IRank> GetRecommenders(IEnumerable<IRank> programmers);
    }
}
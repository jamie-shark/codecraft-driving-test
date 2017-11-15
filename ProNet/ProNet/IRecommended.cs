using System.Collections.Generic;

namespace ProNet
{
    public interface IRecommended : IRecommend
    {
        string GetId();
        IEnumerable<IRecommended> GetRecommenders(IEnumerable<IRecommended> programmers);
    }
}
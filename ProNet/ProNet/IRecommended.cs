using System.Collections.Generic;

namespace ProNet
{
    public interface IRecommended : IIdentifiable
    {
        IEnumerable<IRecommend> GetRecommenders(IEnumerable<IRecommend> programmers);
    }
}
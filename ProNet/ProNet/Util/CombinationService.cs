using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet.Util
{
    public class CombinationService : IPermutationService
    {
        public IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> set, int size) where T : IEquatable<T>
        {
            if (size == 1)
                return set.Select(item => new[] {item});

            return GetPermutations(set, size - 1)
                .SelectMany( item               => set.Where(i => !item.Contains(i)),
                            (item, combination) => item.Concat(new[] { combination }.AsEnumerable()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet.Util
{
    public class PermutationService : IPermutationService
    {
        public IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> set, int size) where T : IEquatable<T>
        {
            return PermutationsFor(set).Where(x => x.Count() == size);
        }

        private static IEnumerable<IEnumerable<T>> PermutationsFor<T>(IEnumerable<T> set)
        {
            var list = set.ToList();
            var permutations = Enumerable.Range(0, 1 << list.Count)
                .Select(item1 =>
                    Enumerable.Range(0, list.Count)
                        .Where(item2 => (item1 & (1 << item2)) != 0)
                        .Select(item => list[item])
                );
            return permutations.Select(x => x.AsEnumerable());
        }
    }
}
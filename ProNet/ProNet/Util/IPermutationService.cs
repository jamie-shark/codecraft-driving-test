using System;
using System.Collections.Generic;

namespace ProNet.Util
{
    public interface IPermutationService
    {
        IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> set, int size) where T : IEquatable<T>;
    }
}
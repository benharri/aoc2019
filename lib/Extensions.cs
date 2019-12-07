using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019.lib
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> list)
        {
            if (list.Count() == 1) return new[] { list };
            return list.SelectMany(t => Permute(list.Where(x => !x.Equals(t))), (v, p) => p.Prepend(v));
        }
    }
}

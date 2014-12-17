using System.Collections.Generic;
using System.Linq;

namespace SnookerCalculatorLib
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> source, int n)
        {
            return Enumerable.Range(1, n).SelectMany(_ => source);
        }
    }
}

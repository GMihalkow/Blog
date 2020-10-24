using System.Collections.Generic;
using System.Linq;

namespace Blog.Dal.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> OptimizedSkip<T>(this IEnumerable<T> collection, int skipAmount)
        {
            var list = collection.ToList();
            var startIndex = skipAmount;

            if (startIndex < 0)
                startIndex = 0;

            for (int i = startIndex; i < list.Count; i++)
            {
                yield return list[i];
            }
        }
    }
}
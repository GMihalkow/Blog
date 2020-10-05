using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> OptimizedSkip<T>(this IEnumerable<T> collection, int skipAmount)
        {
            var list = collection.ToList();

            for (int i = skipAmount - 1; i < list.Count; i++)
            {
                yield return list[i];
            }
        }
    }
}
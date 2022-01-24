using System.Collections.Generic;
using System.Linq;

namespace BankTask.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;

            if (enumerable is ICollection<T> collection)
                return collection.Count < 1;

            return !enumerable.Any();
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace NuGen.Services.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> collection, int chunkSize)
        {
            var list = collection.ToList();
            while (list.Any())
            {
                yield return list.Take(chunkSize);
                list = list.Skip(chunkSize).ToList();
            }
        }
    }
}
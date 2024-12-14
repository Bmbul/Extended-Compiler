using System.Linq;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class LexemTypeExtensions
    {
        public static bool IsAnyOf<T>(this T entity, params T[] collection)
        {
            return collection.Contains(entity);
        }
        
        public static bool IsAnyOf<T>(this T entity, IEnumerable<T> collection)
        {
            return collection.Contains(entity);
        }
        
        public static bool IsNotAnyOf<T>(this T entity, params T[] collection)
        {
            return !collection.Contains(entity);
        }
    }
}

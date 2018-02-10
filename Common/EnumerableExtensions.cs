using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static string ToLog<T>(this IEnumerable<T> list, string separator = ", ", int take = 5)
        {
            return list.Select(i => i.ToString()).ToLog(separator, take);
        }

        public static string ToLog(this IEnumerable<string> list, string separator = ", ", int take = 5)
        {
            list = list.ToList();
            var listStr = list.Take(take).StringJoin();
            if (list.Count() > take)
            {
                listStr += $" ... ({list.Count()})";
            }

            return listStr;
        }

        public static string StringJoin(this IEnumerable<string> list, string separator = ", ")
        {
            return string.Join(separator, list);
        }

        public static string StringJoin<T>(this IEnumerable<T> list, string separator = ", ")
        {
            return list.Select(i => i.ToString()).StringJoin(separator);
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> list)
        {
            return list.Where(i => i != null);
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int size)
        {
            return list
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value));
        }
    }
}

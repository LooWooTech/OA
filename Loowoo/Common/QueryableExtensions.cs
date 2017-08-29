using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SetPage<T>(this IQueryable<T> query, PageParameter page)
        {
            if (page == null) return query;
            page.RecordCount = query.Count();
            return query.Skip(page.PageSize * (page.PageIndex - 1)).Take(page.PageSize);
        }

        public static IEnumerable<T> SetPage<T>(this IEnumerable<T> query, PageParameter page)
        {
            if (page == null) return query;

            if (page.RecordCount == 0)
            {
                page.RecordCount = query.Count();
            }
            return query.Skip(page.PageSize * (page.PageIndex - 1)).Take(page.PageSize);
        }


        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}

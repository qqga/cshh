using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cshh.Asp.Models.Common
{
    public static class JqGridHelpExtensions
    {
        public static string ToSelectListStr<T>(this IEnumerable<T> list, Func<T, Object> id, Func<T, Object> val)
        => string.Join("; ", list.Select(t => $"{id(t)}:{val(t)}"));
    }
}
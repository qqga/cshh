using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Common.Linq
{
    public static class IQueryableExt
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> data, int rowsInPage, int page)
        {
            int skipRows = rowsInPage * page - rowsInPage;
            IQueryable<T> query = data.Skip(skipRows).Take(rowsInPage);
            return query;
        }       
    }
}
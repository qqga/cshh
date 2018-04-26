using Common.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Common.Asp.JqGrid
{
    public class JqGridResponse<Tq, Tres>
    {
        /// <summary>
        /// Всего записей
        /// </summary>
        public int records { get; }
        public int page { get; }
        /// <summary>
        /// Страниц всего
        /// </summary>
        public int total { get; }
        public IEnumerable<Tres> rows { get; private set; }

        //IQueryable<T> Data { get; }
        //JqGridRequest JqGridRequest { get; }

        public JqGridResponse(JqGridRequest jqGridRequest, IQueryable<Tq> allData, Expression<Func<Tq, object>> defaultSortProperty, Func<string, Type, object> deserializer, Func<Tq, Tres> tresCtor = null)
        {
            records = allData.Count();
            page = jqGridRequest.page;
            total = Math.DivRem(records, jqGridRequest.rows, out int rem);
            if(rem > 0) total++;

            //JqGridRequest = jqGridRequest;
            //Data = allData;

            string defaultSortPropertyName = ExprToOperandName(defaultSortProperty);

            var res = GetRows(jqGridRequest, allData, defaultSortPropertyName, deserializer);

            if(tresCtor != null)
                rows = res.Select(tresCtor);
            else
                rows = res.OfType<Tres>();

        }

        //public JqGridResponse(JqGridRequest jqGridRequest, IQueryable<Tq> allData, Expression<Func<Tq, object>> defaultSortProperty, Func<Tq, Tres> tresCtor = null)
        //{


        //}

        static string ExprToOperandName(Expression<Func<Tq, object>> expr)
        {
            UnaryExpression exprBody = expr.Body as UnaryExpression;
            MemberExpression exprOperand = exprBody.Operand as MemberExpression;
            string name = exprOperand.Member.Name;
            return name;
        }
        static IQueryable<Tq> GetRows(JqGridRequest jqGridRequest, IQueryable<Tq> data, string defaultSortProperty, Func<string, Type, object> deserializer)
        {
            string orderQuery = $"{jqGridRequest.sidx ?? defaultSortProperty} {jqGridRequest.sord}";

            if(jqGridRequest._search)
            {
                //string filterQuery = null;
                Filter filter = deserializer(jqGridRequest.filters, typeof(Filter)) as Filter;
                string queryFilter = filter.ToQueryFilter();
                data = data.Where(queryFilter);
            }

            data = data
                //.Order((IQueryableExt.SortMethod)jqGridRequest.sord, jqGridRequest.sidx ?? defaultSortProperty)
                .OrderBy(orderQuery)
                .Paging(jqGridRequest.rows, jqGridRequest.page)
                //.Where()
                ;

            
            return data;
        }
    }
}
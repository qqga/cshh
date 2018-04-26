using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Asp.JqGrid
{
    public class Filter
    {
        public GroupOperator groupOp { get; set; }
        public Rule[] rules { get; set; }

        //public Filter(string filter)
        //{

        //}

        public string ToQueryFilter()
        {
            var rulesStr = rules.Select(r => r.ToQueryFilter());

            string queryFilter = string.Join($" {groupOp} ", rulesStr);

            return queryFilter;
        }
    }
}
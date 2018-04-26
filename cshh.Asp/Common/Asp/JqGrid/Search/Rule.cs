using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Asp.JqGrid
{
    public class Rule
    {
        public string field { get; set; }
        public SearchOperatot op { get; set; }
        public string data { get; set; }
        public string ToQueryFilter()
        {
            var dataQ = !float.TryParse(data, out float f)? data = $"\"{data}\"": data; // todo post sort type or check model type or...

            switch(op)
            {
                case SearchOperatot.eq: return $"{field} = {dataQ}";
                case SearchOperatot.ne: return $"{field} != {dataQ}";
                case SearchOperatot.lt: return $"{field} < {data}";
                case SearchOperatot.le: return $"{field} <= {data}";
                case SearchOperatot.gt: return $"{field} > {data}";
                case SearchOperatot.ge: return $"{field} >= {data}";
                case SearchOperatot.bw: return $"{field}.StartsWith({data})";
                case SearchOperatot.bn: return $"!{field}.StartsWith({data})";
                case SearchOperatot.@in: return string.Join(" OR ", data.Split(',').Select(d => $"{field} = \"{d}\""));//todo
                case SearchOperatot.ni: return string.Join(" AND ", data.Split(',').Select(d => $"{field} != \"{d}\""));
                case SearchOperatot.ew: return $"{field}.EndsWith({data})";
                case SearchOperatot.en: return $"!{field}.EndsWith({data})";
                case SearchOperatot.cn: return $"{field}.Contains({data})";
                case SearchOperatot.nc: return $"!{field}.Contains({data})";
                case SearchOperatot.nu: return $"{RemoveAfterLastDot(field)} == null";
                case SearchOperatot.nn: return $"{RemoveAfterLastDot(field)} != null";

                default: throw new NotSupportedException($"{op} не поддерживается.");
            }
        }

        public string RemoveAfterLastDot(string str) => Regex.Replace(str, "\\.\\w*$", "");
    }
}
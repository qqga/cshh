using System;
using System.Linq;

namespace Common.Asp.JqGrid
{
    public class JqGridRequest
    {
        //Func<string, Type, object> _deserializer;
        public JqGridRequest()
        {
            //_deserializer = deserializer;            
        }
        /// <summary>
        /// Страница
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Строк на странице
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// jsonp имя функции
        /// </summary>
        public string callback { get; set; }

        public bool _search { get; set; }
        public string searchField { get; set; }
        public string searchString { get; set; }
        public SearchOperatot searchOper { get; set; }
        public string filters { get; set; }
        //public Filter filterObj { get; set; }

        public string nd { get; set; }

        /// <summary>
        /// Поле сортировки
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// Наравление сортировки
        /// </summary>
        public SortDirection sord { get; set; }
        public string cart { get; set; }

        public bool IsSorting => !string.IsNullOrEmpty(sidx);


    }
}
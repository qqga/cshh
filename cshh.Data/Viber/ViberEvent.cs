using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLib;

namespace cshh.Data.Viber
{

    public class ViberEvent : BaseEntity
    {
        public string RequestContent { get; set; }
        public string Params { get; set; }
        public DateTime? DateTimeUTC { get; set; }     
    }
}

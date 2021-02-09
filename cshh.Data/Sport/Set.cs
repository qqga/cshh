using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Sport
{
    public class Set : BaseEntity
    {
        public DateTime Dt { get; set; }
        public int Reps { get; set; }
        public int UserId { get; set; }//todo
        //public object Drill { get; set; }
        //public object Workout { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class Bookmark : BaseEntity
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public int Position { get; set; }

        public int ForeignText_Id { get; set; }
        public ForeignText ForeignText { get; set; }
    }
}

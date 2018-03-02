using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class Language:BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<ForeignText> Texts { get; set; }
        public virtual ICollection<Word> Words { get; set; }
    }
}

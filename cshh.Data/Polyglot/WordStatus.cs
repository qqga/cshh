using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class WordStatus : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<UserWord> UserWords { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class WordType:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Word> Words { get; set; }
        public virtual ICollection<WordDefinition> Definitions { get; set; }

    }
}

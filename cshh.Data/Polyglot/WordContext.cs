using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class WordDefinition : BaseEntity
    {
        public UserProfile UserProfile { get; set; }
        public Word Word { get; set; }
        public string Definition { get; set; }
        public string Example { get; set; }
        public WordType WordType { get; set; }
        public bool Public { get; set; }
    }
}

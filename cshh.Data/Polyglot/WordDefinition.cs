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
        public string Definition { get; set; }
        public string Example { get; set; }
        public bool Public { get; set; }

        public UserProfile UserProfile { get; set; }
        public int? UserProfile_Id { get; set; }

        public Word Word { get; set; }
        public int Word_Id { get; set; }


        public WordType Type { get; set; }
        public int? Type_Id { get; set; }

    }
}

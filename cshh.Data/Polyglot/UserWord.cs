using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class UserWord : BaseEntity
    {
        public UserProfile User { get; set; }
        public Word Word { get; set; }
        public WordStatus Status { get; set; }

        public WordSet Set { get; set; }
    }
}

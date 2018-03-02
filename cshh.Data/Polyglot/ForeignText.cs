using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class ForeignText : BaseEntity
    {
        //public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Language Language { get; set; }
        public virtual ICollection<Word> Words { get; set; }

    }
}

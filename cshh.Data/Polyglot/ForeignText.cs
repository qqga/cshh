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
        public string Name { get; set; }
        public string Text { get; set; }
        
        public UserProfile UserProfile { get; set; }
        public int? UserProfile_Id { get; set; }
        
        public Language Language { get; set; }
        public int Language_Id { get; set; }

        public virtual ICollection<Word> Words { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }

    }
}

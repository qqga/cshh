using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.User
{
    //[Table("UserProfiles", Schema = "User")]
    public class UserProfile : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        
        public virtual ICollection<ForeignText> ForeignTexts { get; set; }     
        public virtual ICollection<WordSet> UserWordsSets { get; set; }
        public virtual ICollection<Word> Words { get; set; }
        public virtual ICollection<WordDefinition> WordDefinitions { get; set; }


    }
}

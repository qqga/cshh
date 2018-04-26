using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Polyglot
{
    public class Word : BaseEntity
    {
        public string Text { get; set; }

        public UserProfile UserProfile { get; set; }
        public int UserProfile_Id { get; set; }

        public WordType Type { get; set; }
        public int? Type_Id { get; set; }

        public int Language_Id { get; set; }
        public Language Language { get; set; }

        public DateTime DateTimeCreate { get; set; }

        public virtual ICollection<Word> TranslationsTo { get; set; }
        public virtual ICollection<Word> TranslationsFrom { get; set; }
        public virtual ICollection<ForeignText> Texts { get; set; }
        public virtual ICollection<WordDefinition> Definitions { get; set; }
        public virtual ICollection<UserWord> UserWords { get; set; }


    }
}

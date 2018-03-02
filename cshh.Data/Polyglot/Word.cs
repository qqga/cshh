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
        //public int LanguageId { get; set; }
        public Language Language { get; set; }
        public virtual ICollection<Word> TranslationsTo { get; set; }
        public virtual ICollection<Word> TranslationsFrom { get; set; }
        public virtual ICollection<ForeignText> Texts { get; set; }
        public virtual ICollection<WordDefinition> Definitions { get; set; }
        public virtual ICollection<UserWord> UserWords { get; set; }
        public WordType WordType { get; set; }

    }
}

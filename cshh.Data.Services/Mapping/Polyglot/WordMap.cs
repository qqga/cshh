using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class WordMap : BaseEntityTypeConfiguration<Word>
    {
        public WordMap() : base()
        {
            ToTable("Word", "Polyglot");

            Property(w => w.Text).IsRequired().HasMaxLength(200);

            HasRequired(w => w.Language).WithMany(l => l.Words);
            HasOptional(w => w.WordType).WithMany(wt => wt.Words);

            HasMany(w => w.TranslationsTo).WithMany(w => w.TranslationsFrom);
            HasMany(w => w.Texts).WithMany(t=>t.Words);
            HasMany(w => w.Definitions).WithRequired(c => c.Word);
            HasMany(w => w.UserWords).WithRequired(uw => uw.Word);
            
        }
    }
}

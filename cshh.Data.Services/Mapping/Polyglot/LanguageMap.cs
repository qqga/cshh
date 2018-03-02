using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class LanguageMap : BaseEntityTypeConfiguration<Language>
    {
        public LanguageMap() : base()
        {
            ToTable("Language", "Polyglot");

            Property(l=>l.Name).IsRequired().HasMaxLength(200);
            Property(l => l.ShortName).IsRequired().HasMaxLength(2);

            HasMany(l => l.Texts).WithRequired(t=>t.Language);
            HasMany(l => l.Words).WithRequired(w => w.Language);
        }
    }
}

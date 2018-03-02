using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class WordSetMap : BaseEntityTypeConfiguration<WordSet>
    {
        public WordSetMap() : base()
        {
            ToTable("WordSet", "Polyglot");

            Property(ws => ws.Name).IsRequired().HasMaxLength(100).IsUnicode();

            HasMany(ws => ws.UserWords).WithRequired(uw => uw.Set);
        }
    }
}

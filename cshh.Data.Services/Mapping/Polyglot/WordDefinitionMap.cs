using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class WordDefinitionMap : BaseEntityTypeConfiguration<WordDefinition>
    {
        public WordDefinitionMap() : base()
        {
            Property(w => w.Example).HasMaxLength(2000);
            Property(w => w.Definition).HasMaxLength(2000);

            HasRequired(w => w.Word).WithMany(w => w.Definitions);
            HasOptional(c => c.WordType).WithMany(wt => wt.Definitions);
            HasOptional(d => d.UserProfile).WithMany(u => u.WordDefinitions);
        }
    }
}

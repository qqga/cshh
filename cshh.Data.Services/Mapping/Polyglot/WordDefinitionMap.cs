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
            ToTable("WordDefinition", "Polyglot");

            Property(w => w.Example).HasMaxLength(2000);
            Property(w => w.Definition).HasMaxLength(2000);

            HasRequired(d => d.Word).WithMany(w => w.Definitions).HasForeignKey(d => d.Word_Id);
            HasOptional(d => d.Type).WithMany(wt => wt.Definitions).HasForeignKey(d => d.Type_Id);
            HasOptional(d => d.UserProfile).WithMany(u => u.WordDefinitions).HasForeignKey(d => d.UserProfile_Id);
        }
    }
}

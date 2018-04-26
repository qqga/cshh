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

            HasRequired(w => w.Language).WithMany(l => l.Words).HasForeignKey(w=>w.Language_Id).WillCascadeOnDelete(false);
            HasOptional(w => w.Type).WithMany(wt => wt.Words).HasForeignKey(w=>w.Type_Id);

            HasRequired(w => w.UserProfile).WithMany(up => up.Words).HasForeignKey(w => w.UserProfile_Id).WillCascadeOnDelete(false);

            HasMany(w => w.TranslationsTo).WithMany(w => w.TranslationsFrom).Map(c => c.ToTable("WordWord", "Polyglot"));
            HasMany(w => w.Texts).WithMany(t=>t.Words).Map(c=>c.ToTable("ForeignTextWord", "Polyglot"));

            HasMany(w => w.Definitions).WithRequired(c => c.Word).WillCascadeOnDelete();
            HasMany(w => w.UserWords).WithRequired(uw => uw.Word).WillCascadeOnDelete();
            
        }
    }
}

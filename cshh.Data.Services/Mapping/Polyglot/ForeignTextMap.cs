using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class ForeignTextMap : BaseEntityTypeConfiguration<ForeignText>
    {

        public ForeignTextMap() : base()
        {            
            ToTable("ForeignText", "Polyglot");

            Property(t => t.Name).IsRequired().HasMaxLength(200).IsUnicode();
            Property(t => t.Text).IsRequired().IsMaxLength().IsUnicode();            

            HasOptional(t => t.UserProfile).WithMany(u => u.ForeignTexts).HasForeignKey(t=>t.UserProfile_Id);
            HasRequired(t => t.Language).WithMany(l => l.Texts).HasForeignKey(t=>t.Language_Id).WillCascadeOnDelete(false);
            
            HasMany(t => t.Words).WithMany(w => w.Texts).Map(c => c.ToTable("ForeignTextWord", "Polyglot"));

            HasMany(t => t.Bookmarks).WithRequired(b => b.ForeignText).HasForeignKey(b => b.ForeignText_Id);
        }
    }
}

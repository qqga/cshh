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

            HasRequired(t => t.UserProfile).WithMany(u => u.ForeignTexts);
            HasRequired(t => t.Language).WithMany(l => l.Texts);
            
            HasMany(t => t.Words).WithMany(w => w.Texts);

        }
    }
}

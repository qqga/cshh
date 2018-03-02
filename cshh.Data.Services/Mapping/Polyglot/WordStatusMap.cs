using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    class WordStatusMap:BaseEntityTypeConfiguration<WordStatus>
    {
        public WordStatusMap():base()
        {
            ToTable("WordStatus", "Polyglot");

            Property(s => s.Name).IsRequired().HasMaxLength(200).IsUnicode();

            HasMany(s => s.UserWords).WithRequired(uw=>uw.Status);
        }
    }
}

using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Viber
{
    class ViberEventMap : BaseEntityTypeConfiguration<cshh.Data.Viber.ViberEvent>
    {

        public ViberEventMap() : base()
        {
            ToTable("Event", "Viber");
            Property(e => e.RequestContent).HasMaxLength(4000).IsUnicode();
            Property(e => e.DateTimeUTC).HasPrecision(0);
        }
    }
}

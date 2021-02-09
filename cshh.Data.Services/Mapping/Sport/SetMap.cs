using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Sport
{
    class SetMap : BaseEntityTypeConfiguration<cshh.Data.Sport.Set>
    {

        public SetMap() : base()
        {
            ToTable("Set", "Sport");
            Property(t => t.Dt).IsRequired();
            Property(t => t.Reps).IsRequired();
        }
    }
}

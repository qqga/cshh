using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping
{
    class BaseEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseEntityTypeConfiguration()
        {
            this.HasKey(uw => uw.Id)
                .Property(uw => uw.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

        }
    }
}

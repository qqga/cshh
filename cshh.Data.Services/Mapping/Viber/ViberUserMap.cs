using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Viber
{
    class ViberUserMap : BaseEntityTypeConfiguration<cshh.Data.Viber.ViberUser>
    {

        public ViberUserMap() : base()
        {
            ToTable("User", "Viber");

            HasOptional(vu => vu.UserProfile).WithMany(u=>u.ViberUsers).HasForeignKey(vu=>vu.UserProfile_Id);
        }
    }
}

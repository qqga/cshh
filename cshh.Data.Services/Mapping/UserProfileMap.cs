using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping
{
    internal class UserProfileMap : BaseEntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            ToTable("UserProfile", "Polyglot");

            Property(u => u.ApplicationUserId).IsRequired();

            HasMany(u => u.UserWords).WithRequired(uw => uw.User);
            HasMany(u => u.ForeignTexts).WithRequired(t => t.UserProfile);
     
        }
    }
}

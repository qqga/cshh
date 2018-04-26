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
            ToTable("UserProfile", "dbo");

            Property(u => u.ApplicationUserId).IsRequired().HasMaxLength(128);            
            //HasIndex(up => up.ApplicationUserId).IsUnique();  error: constructor not found

            HasMany(u => u.UserWordsSets).WithRequired(uw => uw.User);
            HasMany(u => u.ForeignTexts).WithOptional(t => t.UserProfile).HasForeignKey(t=>t.UserProfile_Id).WillCascadeOnDelete(false);
            HasMany(u => u.Words).WithRequired(w => w.UserProfile).HasForeignKey(w => w.UserProfile_Id).WillCascadeOnDelete(false);
        }
    }
}

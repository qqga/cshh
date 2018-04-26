using cshh.Data.Polyglot;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Mapping.Polyglot
{
    internal class UserWordMap : BaseEntityTypeConfiguration<UserWord>
    {
        public UserWordMap() : base()
        {
            ToTable("UserWord", "Polyglot");

            HasRequired(uw => uw.Word).WithMany(w => w.UserWords).HasForeignKey(w=>w.Word_Id);
            HasRequired(uw => uw.Set).WithMany(u => u.UserWords).HasForeignKey(w=>w.Set_Id);        
            HasRequired(uw => uw.Status).WithMany(s => s.UserWords).HasForeignKey(w=>w.Status_Id);
            //HasRequired(uw => uw.Set);
        }
    }
}

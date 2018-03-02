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
            HasRequired(uw => uw.Word).WithMany(w => w.UserWords);
            HasRequired(uw => uw.User).WithMany(u => u.UserWords);        
            HasRequired(uw => uw.Status).WithMany(s => s.UserWords);
            HasRequired(uw => uw.Set);
        }
    }
}

using cshh.Data.Polyglot;
using cshh.Data.Services.DbContexts;
using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public class ViberEfRepository : EfRepository, IViberRepository
    {
        public ViberEfRepository(ViberDbContext context) : base(context)
        {

        }

        #region RegionName
        
        public IQueryable<cshh.Data.Viber.ViberEvent> ViberEvents => Context.Set<cshh.Data.Viber.ViberEvent>();
        public IQueryable<cshh.Data.Viber.ViberUser> ViberUsers => Context.Set<cshh.Data.Viber.ViberUser>();

        public IQueryable<UserProfile> UserProfiles => Context.Set<UserProfile>();
        #endregion
    }
}

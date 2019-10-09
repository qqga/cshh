using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public interface IViberRepository : IUserRepository
    {
        IQueryable<cshh.Data.Viber.ViberEvent> ViberEvents { get;  }
        IQueryable<cshh.Data.Viber.ViberUser> ViberUsers { get; }
    }
}

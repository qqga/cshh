using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public interface ITaskRepository : IUserRepository
    {
        IQueryable<Data.Tasks.Task> Tasks { get; }
        //IQueryable<UserProfile> UserProfiles { get; }
    }
}

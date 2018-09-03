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
    public class TaskEfRepository : EfRepository, ITaskRepository
    {
        public TaskEfRepository(TaskDbContext context) : base(context)
        {

        }

        #region RegionName
        public IQueryable<Data.Tasks.Task> Tasks => Context.Set<Data.Tasks.Task>();
        public IQueryable<UserProfile> UserProfiles => Context.Set<UserProfile>();
        #endregion
    }
}

using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Data.Services.Repo
{
    public interface IUserRepository : IRepository
    {
        IQueryable<UserProfile> UserProfiles { get; }
    }

    public static class IUserRepositoryExt
    {
        public static UserProfile GetUserProfile(this IUserRepository userRepo, string appUserId) =>
            userRepo.FindBy<UserProfile>(up => up.ApplicationUserId == appUserId).FirstOrDefault();

    }
}

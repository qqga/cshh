using cshh.Data.Services.Repo;
using cshh.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services.User
{
    public interface IUserService : IDisposable
    {
        IUserRepository UserRepository { get; set; }
        UserProfile AddUserProfile(string appUserId);
    }

    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public UserProfile AddUserProfile(string appUserId)
        {
            UserProfile existingProfile = UserRepository.UserProfiles.Where(up => up.ApplicationUserId == appUserId).FirstOrDefault();
            if(existingProfile != null)
                throw new InvalidOperationException($"Userprofile with appid({appUserId}) already exsits.");

            UserProfile userProfile = new UserProfile() { ApplicationUserId = appUserId };
            UserRepository.Add(userProfile, true);
            return userProfile;
        }

        public void Dispose()
        {
            UserRepository.Dispose();
        }
    }
}

using Contracts;
using HishabNikash.Models;
using Service.Contracts;

namespace Service
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager repositoryManager;

        public UserService(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public async Task<User> AddUser(User user)
        {
            var _user = await repositoryManager.User.AddUser(user);
            repositoryManager.Save();
            return _user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await repositoryManager.User.GetAllUsersAsync();
            return users;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var user = await repositoryManager.User.GetUserByIDAsync(userId);
            return user;
        }
    }
}

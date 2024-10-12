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

        public void AddUser(User user)
        {
            repositoryManager.User.AddUser(user);
            repositoryManager.Save();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await repositoryManager.User.GetAllUsersAsync();
            return users;
        }
    }
}

using Contracts;
using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void AddUser(User user)
        {
            Create(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var allUsers = await FindAll().ToListAsync();
            
            return allUsers;
        }

        public async Task<User?> GetUserByIDAsync(int userID)
        {
            var user = await FindByCondition(_user => _user.ID == userID)
                .FirstOrDefaultAsync();

            return user;
        }

        public bool IsUserAlreadyRegistered(string username, string email)
        {
            var isExistUser = FindByCondition(u => u.UserName == username).Any();
            var isExistEmail = FindByCondition(u => u.Email == email).Any();
            return (isExistUser || isExistEmail);
        }

        public bool IsUserExistAsync(int userID)
        {
            return FindByCondition(u => u.ID == userID).Any();
        }
    }
}

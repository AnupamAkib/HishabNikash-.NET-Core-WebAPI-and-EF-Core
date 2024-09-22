using HishabNikash.Context;
using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;

namespace HishabNikash.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDBContext dbContext;

        public UserRepository(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var allUsers = await dbContext.Users.ToListAsync();
            return allUsers;
        }

        public async Task<User?> GetUserByIDAsync(int userID)
        {
            var user = await dbContext.Users
                .Where(_user => _user.UserID == userID)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> IsUserAlreadyRegistered(string username, string email)
        {
            bool isExistUsername = await dbContext.Users.AnyAsync(u => u.UserName == username);
            bool isExistEmail = await dbContext.Users.AnyAsync(u => u.Email == email);
            return (isExistUsername || isExistEmail);
        }

        public async Task<bool> IsUserExistAsync(int userID)
        {
            return await dbContext.Users.AnyAsync(u => u.UserID == userID);
        }
    }
}

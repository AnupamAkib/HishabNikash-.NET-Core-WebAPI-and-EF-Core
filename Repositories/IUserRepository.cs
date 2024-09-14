using HishabNikash.Context;
using HishabNikash.Models;

namespace HishabNikash.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIDAsync(int userID);
    }
}

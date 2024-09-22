using HishabNikash.Context;
using HishabNikash.Models;

namespace HishabNikash.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIDAsync(int userID);
        Task<bool> IsUserAlreadyRegistered(string username, string email);
        Task<bool> IsUserExistAsync(int userID);
    }
}

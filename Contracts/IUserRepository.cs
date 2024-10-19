using HishabNikash.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIDAsync(int userID);
        bool IsUserAlreadyRegistered(string username, string email);
        bool IsUserExistAsync(int userID);
    }
}

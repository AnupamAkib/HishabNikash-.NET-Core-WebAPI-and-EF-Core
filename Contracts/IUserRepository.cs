using HishabNikash.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        void AddUser(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIDAsync(int userID);
        bool IsUserAlreadyRegistered(string username, string email);
        bool IsUserExistAsync(int userID);
    }
}

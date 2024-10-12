using HishabNikash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IUserService
    {
        void AddUser(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}

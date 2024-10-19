using HishabNikash.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace HishabNikash.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(User user)
        {
            var _user = await serviceManager.UserService.AddUser(user);
            return Ok(_user);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await serviceManager.UserService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await serviceManager.UserService.GetUserByIdAsync(id);
            return Ok(user);
        }
    }
}

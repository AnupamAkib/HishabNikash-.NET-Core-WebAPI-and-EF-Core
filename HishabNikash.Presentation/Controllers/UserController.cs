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
        public IActionResult AddUser(User user)
        {
            serviceManager.UserService.AddUser(user);
            return Ok(user);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await serviceManager.UserService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}

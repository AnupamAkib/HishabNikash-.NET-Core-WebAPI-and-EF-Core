using HishabNikash.Context;
using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
using HishabNikash.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace HishabNikash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService) 
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> RegisterUserAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            var userResponseDTO = await userService.RegisterNewUserAsync(registrationRequestDTO);
            return Ok(userResponseDTO);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserByID")]
        public async Task<IActionResult> GetUserByIDAsync(int userID)
        {
            var user = await userService.GetUserByIDAsync(userID);
            return Ok(user);
        }
    }
}

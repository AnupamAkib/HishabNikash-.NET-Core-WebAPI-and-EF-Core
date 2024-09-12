using HishabNikash.Context;
using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
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
        private IConfiguration configuration;
        private ApplicationDBContext dbContext;
        public UserController(IConfiguration configuration, ApplicationDBContext dbContext) 
        { 
            this.configuration = configuration;
            this.dbContext = dbContext;
        }


        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> AddUser(RegistrationRequestDTO requestPayload)
        {
            if(requestPayload == null)
            {
                return BadRequest("User payload can't be null");
            }

            var user = new User
            {
                FirstName = requestPayload.FirstName,
                LastName = requestPayload.LastName,
                UserName = requestPayload.UserName,
                Email = requestPayload.Email,
                HashedPassword = requestPayload.HashedPassword
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            var responsePayload = new UserResponseDTO{
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                HashedPassword = user.HashedPassword,
                CreatedDate = user.CreatedDate
            };

            return Ok(responsePayload);
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await dbContext.Users
                .Select(user => new UserResponseDTO
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    HashedPassword = user.HashedPassword,
                    CreatedDate = user.CreatedDate
                }).ToListAsync();

            return Ok(allUsers);
        }


        [HttpGet]
        [Route("GetUserByID")]
        public async Task<IActionResult> GetUserByID(int userID)
        {
            var user = await dbContext.Users
                .Where(_user => _user.UserID == userID)
                .Select(u => new UserResponseDTO
                {
                    UserID = u.UserID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    FullName = u.FullName,
                    UserName = u.UserName,
                    Email = u.Email,
                    HashedPassword = u.HashedPassword,
                    CreatedDate = u.CreatedDate
                }).FirstOrDefaultAsync();

            if(user == null)
            {
                return NotFound($"User with id {userID} not found!");
            }
            return Ok(user);
        }
    }
}

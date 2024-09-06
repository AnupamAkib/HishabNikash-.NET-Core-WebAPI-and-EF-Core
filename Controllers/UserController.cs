using HishabNikash.Context;
using HishabNikash.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddUser(User user)
        {
            if(user is not null)
            {
                var addedUser = dbContext.Users.Add(user);
                int affectedRows = await dbContext.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await dbContext.Users.ToListAsync();
            return Ok(allUsers);
        }
    }
}

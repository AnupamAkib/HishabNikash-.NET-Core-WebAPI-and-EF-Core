using HishabNikash.Context;
using HishabNikash.Exceptions;
using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
using HishabNikash.Services;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var userResponseDTO = await userService.RegisterNewUserAsync(registrationRequestDTO);
                return Ok(userResponseDTO);
            }
            catch(CustomException ex) when (ex is InvalidInputException || ex is AlreadyExistException)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                var user = await userService.GetUserByIDAsync(userID);
                return Ok(user);
            }
            catch (CustomException ex) when (ex is NotFoundException)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

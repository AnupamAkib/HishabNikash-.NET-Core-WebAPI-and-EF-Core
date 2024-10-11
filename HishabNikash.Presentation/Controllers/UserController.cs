using Microsoft.AspNetCore.Mvc;

namespace HishabNikash.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Message()
        {
            return Ok(new { msg = "working User"});
        }
    }
}

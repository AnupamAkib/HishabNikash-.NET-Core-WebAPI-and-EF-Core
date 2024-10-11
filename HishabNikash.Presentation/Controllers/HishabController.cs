using Microsoft.AspNetCore.Mvc;

namespace HishabNikash.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HishabController : ControllerBase
    {
        [HttpGet]
        public IActionResult Message()
        {
            return Ok(new { msg = "working Hishab" });
        }
    }
}

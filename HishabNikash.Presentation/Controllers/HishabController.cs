using HishabNikash.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace HishabNikash.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HishabController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public HishabController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewHishab(Hishab hishab)
        {
            var _hishab = await serviceManager.HishabService.CreateNewHishab(hishab);
            return Ok(new
            {
                message = "success",
                content = _hishab
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetHishabByIDAsync(int hishabID)
        {
            var hishab = await serviceManager.HishabService.GetHishabByIDAsync(hishabID);
            return Ok(new
            {
                message = "success",
                content = hishab
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetHishabsByUserAsync(int userID)
        {
            var hishabs = await serviceManager.HishabService.GetHishabsByUserAsync(userID);
            return Ok(new
            {
                message = "success",
                content = hishabs
            });
        }

        [HttpPut]
        public async Task<IActionResult> EditHishab(int hishabID, string updatedHishabName, string updatedCardColor)
        {
            var hishab = await serviceManager.HishabService.EditHishab(hishabID, updatedHishabName, updatedCardColor);
            return Ok(new
            {
                message = "success",
                content = hishab
            });
        }

        [HttpPut]
        public IActionResult IncreaseAmount(int hishabID, int amount)
        {
            var result = serviceManager.HishabService.IncreaseAmount(hishabID, amount);
            if (result)
            {
                return Ok(new { message = "success" });
            }
            else
            {
                return BadRequest(new { message = "failed" });
            }
        }

        [HttpPut]
        public IActionResult DecreaseAmount(int hishabID, int amount)
        {
            bool result = serviceManager.HishabService.DecreaseAmount(hishabID, amount);
            if (result)
            {
                return Ok(new { message = "success" });
            }
            else
            {
                return BadRequest(new { message = "failed" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHishab(int hishabID)
        {
            bool result = await serviceManager.HishabService.DeleteHishab(hishabID);
            if (result)
            {
                return Ok(new
                {
                    message = "success"
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = "failed"
                });
            }
        }
    }
}

using HishabNikash.Context;
using HishabNikash.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HishabNikash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HishabController : ControllerBase
    {
        private IConfiguration configuration;
        private ApplicationDBContext dbContext;

        public HishabController(IConfiguration configuration, ApplicationDBContext dBContext)
        {
            this.configuration = configuration;
            this.dbContext = dBContext;
        }

        [HttpPost]
        [Route("CreateNewHishab")]
        public async Task<IActionResult> CreateNewHishab(Hishab hishab)
        {
            if (hishab is not null)
            {
                dbContext.Hishabs.Add(hishab);
                await dbContext.SaveChangesAsync();
                return Ok(hishab);
            }
            else
            {
                return BadRequest("Hishab is null");
            }
        }

        [HttpGet]
        [Route("GetHishabsByUser")]
        public async Task<IActionResult> GetHishabsByUser(int UserID)
        {
            try
            {
                var userExists = await dbContext.Users.AnyAsync(u => u.UserID == UserID);

                if (!userExists)
                {
                    return NotFound($"User with ID {UserID} does not exist.");
                }

                var hishabs = await dbContext.Hishabs.Where(hishab => hishab.UserID == UserID).Include(h => h.Histories).ToListAsync();
                return Ok(hishabs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetHishabByID")]
        public async Task<IActionResult> GetHishabByID(int hishabID)
        {
            try
            {
                var hishab = await dbContext.Hishabs.Where(hishab => hishab.HishabID == hishabID).Include(h => h.Histories).ToListAsync();
                if(hishab is null || hishab.Count == 0)
                {
                    return NotFound($"Hishab with ID {hishabID} does not exist.");
                }
                return Ok(hishab);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Credit")]
        public async Task<IActionResult> IncreaseAmount(int hishabID, int value)
        {
            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == hishabID);
            if (hishab is not null)
            {
                hishab.Amount += value;

                History history = new History();
                history.HistoryName = $"{value} taka has been credited";
                history.HistoryType = "credited";
                hishab.Histories?.Add(history);

                await dbContext.SaveChangesAsync();
                return Ok(hishab);
            }
            else
            {
                return BadRequest($"Hishab not found for hishab id {hishabID}");
            }
        }

        [HttpPut]
        [Route("Debit")]
        public async Task<IActionResult> DecreaseAmount(int hishabID, int value)
        {
            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == hishabID);
            if (hishab is not null)
            {
                hishab.Amount -= value;

                History history = new History();
                history.HistoryName = $"{value} taka has been debited";
                history.HistoryType = "debited";
                hishab.Histories?.Add(history);

                await dbContext.SaveChangesAsync();
                return Ok(hishab);
            }
            else
            {
                return BadRequest($"Hishab not found for hishab id {hishabID}");
            }
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> EditHishab(int hishabID, string newName, string newColor) //use DTO later
        {
            var hishab = dbContext.Hishabs.FirstOrDefault(h => h.HishabID == hishabID);
            if(hishab is not null)
            {
                hishab.Name = newName;
                hishab.CardColor = newColor;
                await dbContext.SaveChangesAsync();
                return Ok("Hishab updated!");
            }
            else
            {
                return BadRequest("something went wrong");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteHishab(int hishabID)
        {
            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == hishabID);
            if (hishab is not null)
            {
                dbContext.Hishabs.Remove(hishab);
                await dbContext.SaveChangesAsync();
                return Ok($"Hishab with ID {hishabID} has been deleted.");
            }
            else
            {
                return BadRequest("Hishab not found");
            }
        }
    }
}

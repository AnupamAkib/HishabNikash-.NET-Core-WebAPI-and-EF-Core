using HishabNikash.Context;
using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
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
        public async Task<IActionResult> CreateNewHishab(CreateHishabRequestPayload hishabPayload)
        {
            if (hishabPayload is null)
            {
                return BadRequest("hishab payload is null");
            }

            var userExist = await dbContext.Users.AnyAsync(u => u.UserID == hishabPayload.UserID);

            if (!userExist)
            {
                return NotFound($"User not found with id {hishabPayload.UserID}");
            }

            var hishab = new Hishab
            {
                UserID = hishabPayload.UserID,
                Name = hishabPayload.Name,
                Amount = hishabPayload.Amount,
                CardColor = hishabPayload.CardColor
            };

            hishab.Histories?.Add(new History
            {
                HistoryName = $"Hishab created with initial amount of {hishab.Amount}",
                HistoryType = "others"
            });

            dbContext.Hishabs.Add(hishab);
            await dbContext.SaveChangesAsync();

            var hishabResponsePayload = new HishabResponsePayload
            {
                HishabID = hishab.HishabID,
                UserID = hishab.UserID,
                Name = hishab.Name,
                Amount = hishab.Amount,
                CardColor = hishab.CardColor,
                TransactionHistories = hishab.Histories != null
                    ? hishab.Histories.Select(his => new HistoryResponsePayload
                    {
                        HistoryName = his.HistoryName,
                        HistoryType = his.HistoryType,
                        CreatedDate = his.CreatedDate.ToString()
                    })
                    .ToList()
                    : new List<HistoryResponsePayload>()
            };

            return Ok(hishabResponsePayload);
        }


        [HttpGet]
        [Route("GetHishabsByUser")]
        public async Task<IActionResult> GetHishabsByUser(int userID)
        {
            try
            {
                var userExists = await dbContext.Users.AnyAsync(u => u.UserID == userID);

                if (!userExists)
                {
                    return NotFound($"User with ID {userID} does not exist.");
                }

                var hishabs = await dbContext.Hishabs
                    .Where(hishab => hishab.UserID == userID)
                    .Include(h => h.Histories)
                    .Select(_hishab => new HishabResponsePayload
                    {
                        HishabID = _hishab.HishabID,
                        UserID = _hishab.UserID,
                        Name = _hishab.Name,
                        Amount = _hishab.Amount,
                        CardColor = _hishab.CardColor,
                        TransactionHistories = _hishab.Histories != null
                            ? _hishab.Histories.Select(his => new HistoryResponsePayload
                            {
                                HistoryName = his.HistoryName,
                                HistoryType = his.HistoryType,
                                CreatedDate = his.CreatedDate.ToString()
                            })
                            .ToList() 
                            : new List<HistoryResponsePayload>()
                    })
                    .ToListAsync();

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
                var hishab = await dbContext.Hishabs
                    .Where(hishab => hishab.HishabID == hishabID)
                    .Include(h => h.Histories)
                    .Select(_hishab => new HishabResponsePayload
                    {
                        HishabID = _hishab.HishabID,
                        UserID = _hishab.UserID,
                        Name = _hishab.Name,
                        Amount = _hishab.Amount,
                        CardColor = _hishab.CardColor,
                        TransactionHistories = _hishab.Histories != null
                            ? _hishab.Histories.Select(history => new HistoryResponsePayload
                            {
                                HistoryName = history.HistoryName,
                                HistoryType = history.HistoryType,
                                CreatedDate = history.CreatedDate.ToString()
                            }).ToList()
                            : new List<HistoryResponsePayload>()
                    })
                    .ToListAsync();

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

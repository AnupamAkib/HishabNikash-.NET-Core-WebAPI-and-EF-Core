using HishabNikash.Context;
using HishabNikash.DTOs.RequestsDTO;
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
        public async Task<IActionResult> CreateNewHishab(CreateHishabRequestDTO hishabPayload)
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
                HistoryName = $"Hishab created with initial amount of {hishab.Amount} taka",
                HistoryType = "others"
            });

            dbContext.Hishabs.Add(hishab);
            await dbContext.SaveChangesAsync();

            var hishabResponsePayload = new HishabResponseDTO
            {
                HishabID = hishab.HishabID,
                UserID = hishab.UserID,
                Name = hishab.Name,
                Amount = hishab.Amount,
                CardColor = hishab.CardColor,
                TransactionHistories = hishab.Histories != null
                    ? hishab.Histories.Select(his => new HistoryResponseDTO
                    {
                        HistoryName = his.HistoryName,
                        HistoryType = his.HistoryType,
                        CreatedDate = his.CreatedDate.ToString()
                    })
                    .ToList()
                    : new List<HistoryResponseDTO>()
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
                    .Select(_hishab => new HishabResponseDTO
                    {
                        HishabID = _hishab.HishabID,
                        UserID = _hishab.UserID,
                        Name = _hishab.Name,
                        Amount = _hishab.Amount,
                        CardColor = _hishab.CardColor,
                        TransactionHistories = _hishab.Histories != null
                            ? _hishab.Histories.Select(his => new HistoryResponseDTO
                            {
                                HistoryName = his.HistoryName,
                                HistoryType = his.HistoryType,
                                CreatedDate = his.CreatedDate.ToString()
                            })
                            .ToList() 
                            : new List<HistoryResponseDTO>()
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
                    .Select(_hishab => new HishabResponseDTO
                    {
                        HishabID = _hishab.HishabID,
                        UserID = _hishab.UserID,
                        Name = _hishab.Name,
                        Amount = _hishab.Amount,
                        CardColor = _hishab.CardColor,
                        TransactionHistories = _hishab.Histories != null
                            ? _hishab.Histories.Select(history => new HistoryResponseDTO
                            {
                                HistoryName = history.HistoryName,
                                HistoryType = history.HistoryType,
                                CreatedDate = history.CreatedDate.ToString()
                            }).ToList()
                            : new List<HistoryResponseDTO>()
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
        public async Task<IActionResult> IncreaseAmount(HishabAmountUpdateRequestDTO requestPayload)
        {
            if(requestPayload is null)
            {
                return BadRequest("UpdateAmountRequestPayload is null");
            }
            
            if(requestPayload.Amount <= 0)
            {
                return BadRequest("Invalid amount. Please enter a positive value");
            }

            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == requestPayload.HishabID);

            if (hishab is null)
            {
                return BadRequest($"Hishab not found for hishab id {requestPayload.HishabID}");
            }

            hishab.Amount += requestPayload.Amount;

            var history = new History
            {
                HistoryName = $"{requestPayload.Amount} taka has been credited",
                HistoryType = "credited"
            };

            hishab.Histories?.Add(history);
            await dbContext.SaveChangesAsync();

            var hishabUpdateResponse = new HishabAmountUpdateResponseDTO
            {
                HishabID = hishab.HishabID,
                UserID = hishab.UserID,
                Name = hishab.Name,
                UpdatedAmount = hishab.Amount
            };

            return Ok(hishabUpdateResponse);
        }

        [HttpPut]
        [Route("Debit")]
        public async Task<IActionResult> DecreaseAmount(HishabAmountUpdateRequestDTO requestPayload)
        {
            if (requestPayload is null)
            {
                return BadRequest("HishabAmountUpdateRequestDTO is null");
            }

            if (requestPayload.Amount <= 0)
            {
                return BadRequest("Invalid amount. Please enter a positive value");
            }

            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == requestPayload.HishabID);

            if (hishab is null)
            {
                return BadRequest($"Hishab not found for hishab id {requestPayload.HishabID}");
            }

            hishab.Amount -= requestPayload.Amount;

            var history = new History
            {
                HistoryName = $"{requestPayload.Amount} taka has been debited",
                HistoryType = "debited"
            };

            hishab.Histories?.Add(history);
            await dbContext.SaveChangesAsync();

            var hishabUpdateResponse = new HishabAmountUpdateResponseDTO
            {
                HishabID = hishab.HishabID,
                UserID = hishab.UserID,
                Name = hishab.Name,
                UpdatedAmount = hishab.Amount
            };

            return Ok(hishabUpdateResponse);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> EditHishab(EditHishabRequestDTO requestDto) //use DTO later
        {
            var hishab = dbContext.Hishabs.FirstOrDefault(h => h.HishabID == requestDto.HishabID);
            if(hishab is not null)
            {
                hishab.Name = requestDto.UpdatedHishabName;
                hishab.CardColor = requestDto.UpdatedCardColor;
                await dbContext.SaveChangesAsync();
                return Ok(new
                {
                    status = "success",
                    updatedData = requestDto
                });
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

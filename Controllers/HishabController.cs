using HishabNikash.Context;
using HishabNikash.DTOs.RequestsDTO;
using HishabNikash.Exceptions;
using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
using HishabNikash.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HishabNikash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HishabController : ControllerBase
    {
        private readonly IHishabService hishabService;

        public HishabController(IHishabService hishabService)
        {
            this.hishabService = hishabService;
        }
        /*
         * Add exception throwing in User Services/controllers
         * Check if we really need nullable property (?) in Entries / Solve them accordingly
         */

        [HttpPost]
        [Route("CreateNewHishab")]
        public async Task<IActionResult> CreateNewHishab(CreateHishabRequestDTO hishabRequestDto)
        {
            try
            {
                var hishab = await hishabService.CreateNewHishabAsync(hishabRequestDto);
                return Ok(hishab);
            }
            catch(CustomException ex) when (ex is NotFoundException || ex is InvalidInputException)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetHishabsByUser")]
        public async Task<IActionResult> GetHishabsByUser(int userID)
        {
            try
            {
                var hishab = await hishabService.GetHishabsByUserAsync(userID);
                return Ok(hishab);
            }
            catch(CustomException ex) when (ex is NotFoundException)
            {
                return StatusCode(ex.StatusCode, ex.Message);
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
                var hishab = await hishabService.GetHishabByIDAsync(hishabID);
                return Ok(hishab);
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

        [HttpPut]
        [Route("Credit")]
        public async Task<IActionResult> IncreaseAmount(HishabAmountUpdateRequestDTO hishabAmountUpdateDto)
        {
            try
            {
                var hishab = await hishabService.IncreaseAmountAsync(hishabAmountUpdateDto);
                return Ok(hishab);
            }
            catch (CustomException ex) when (ex is NotFoundException || ex is InvalidInputException)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Debit")]
        public async Task<IActionResult> DecreaseAmount(HishabAmountUpdateRequestDTO hishabAmountUpdateDto)
        {
            try
            {
                var hishab = await hishabService.DecreaseAmountAsync(hishabAmountUpdateDto);
                return Ok(hishab);
            }
            catch (CustomException ex) when (ex is NotFoundException || ex is InvalidInputException)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> EditHishab(EditHishabDTO editHishabDto) //use DTO later
        {
            try
            {
                var updatedHishabDto = await hishabService.EditHishabAsync(editHishabDto);
                return Ok(updatedHishabDto);
            }
            catch (CustomException ex) when (ex is NotFoundException || ex is InvalidInputException)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteHishab(int hishabID)
        {
            bool isDeleted = await hishabService.DeleteHishabAsync(hishabID);

            if (isDeleted)
            {
                return Ok(new
                {
                    message = $"Hishab deleted with ID {hishabID}"
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = "Unsuccessful! Hishab can't be deleted or not found!"
                });
            }
        }
    }
}

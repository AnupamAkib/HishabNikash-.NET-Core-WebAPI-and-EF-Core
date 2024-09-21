using HishabNikash.DTOs.RequestsDTO;
using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;

namespace HishabNikash.Services
{
    public interface IHishabService
    {
        Task<HishabResponseDTO> CreateNewHishabAsync(CreateHishabRequestDTO hishabRequestDto);
        Task<List<HishabResponseDTO>?> GetHishabsByUserAsync(int userID);
        Task<HishabResponseDTO?> GetHishabByIDAsync(int hishabID);
        Task<HishabAmountUpdateResponseDTO?> IncreaseAmountAsync(HishabAmountUpdateRequestDTO hishabAmountUpdateRequestDto);
        Task<HishabAmountUpdateResponseDTO?> DecreaseAmountAsync(HishabAmountUpdateRequestDTO hishabAmountUpdateRequestDto);
        Task<EditHishabDTO?> EditHishabAsync(EditHishabDTO editHishabDto);
        Task<bool> DeleteHishabAsync(int hishabID);
    }
}

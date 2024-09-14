using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
using HishabNikash.Repositories;

namespace HishabNikash.Services
{
    public interface IUserService
    {
        Task<UserResponseDTO> RegisterNewUserAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        Task<UserResponseDTO?> GetUserByIDAsync(int userID);
    }
}

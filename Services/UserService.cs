using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
using HishabNikash.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HishabNikash.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserResponseDTO> RegisterNewUserAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            var registeredUser = await userRepository.AddUserAsync(new User
            {
                FirstName = registrationRequestDTO.FirstName,
                LastName = registrationRequestDTO.LastName,
                UserName = registrationRequestDTO.UserName,
                Email = registrationRequestDTO.Email,
                HashedPassword = registrationRequestDTO.HashedPassword
            });

            var userResponseDTO = new UserResponseDTO
            {
                UserID = registeredUser.UserID,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                FullName = registeredUser.FullName,
                UserName = registeredUser.UserName,
                Email = registeredUser.Email,
                CreatedDate = registeredUser.CreatedDate
            };

            return userResponseDTO;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllUsersAsync();

            var userResponseDTOs = users.Select(user => new UserResponseDTO
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            }).ToList();

            return userResponseDTOs;
        }

        public async Task<UserResponseDTO?> GetUserByIDAsync(int userID)
        {
            var user = await userRepository.GetUserByIDAsync(userID);

            if(user is null)
            {
                return null;
            }

            var userResponseDTO = new UserResponseDTO
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };

            return userResponseDTO;
        }
    }
}

using HishabNikash.DTOs.RequestsDTO;
using HishabNikash.Exceptions;
using HishabNikash.Models;
using HishabNikash.Payloads.Requests;
using HishabNikash.Payloads.Responses;
using HishabNikash.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HishabNikash.Services
{
    public class HishabService : IHishabService
    {
        private readonly IHishabRepository hishabRepository;
        private readonly IUserRepository userRepository;

        public HishabService(IHishabRepository hishabRepository, IUserRepository userRepository)
        {
            this.hishabRepository = hishabRepository;
            this.userRepository = userRepository;
        }

        public async Task<HishabResponseDTO> CreateNewHishabAsync(CreateHishabRequestDTO hishabRequestDto)
        {
            if (hishabRequestDto is null)
            {
                throw new InvalidInputException("Input format is not valid");
            }

            var userExist = await userRepository.IsUserExistAsync(hishabRequestDto.UserID);

            if (!userExist)
            {
                throw new NotFoundException($"User doesn't exist with ID {hishabRequestDto.UserID}");
            }

            var hishab = new Hishab
            {
                UserID = hishabRequestDto.UserID,
                Name = hishabRequestDto.Name,
                Amount = hishabRequestDto.Amount,
                CardColor = hishabRequestDto.CardColor
            };

            hishab.Histories?.Add(new History
            {
                HistoryName = $"Hishab created with initial amount of {hishab.Amount} taka",
                HistoryType = "others"
            });

            var newHishab = await hishabRepository.CreateNewHishabAsync(hishab);

            var hishabResponseDto = new HishabResponseDTO
            {
                HishabID = newHishab.HishabID,
                UserID = newHishab.UserID,
                Name = newHishab.Name,
                Amount = newHishab.Amount,
                CardColor = newHishab.CardColor,
                TransactionHistories = newHishab.Histories != null
                    ? newHishab.Histories.Select(his => new HistoryResponseDTO
                    {
                        HistoryName = his.HistoryName,
                        HistoryType = his.HistoryType,
                        CreatedDate = his.CreatedDate.ToString()
                    })
                    .ToList()
                    : new List<HistoryResponseDTO>()
            };

            return hishabResponseDto;
        }

        public async Task<List<HishabResponseDTO>?> GetHishabsByUserAsync(int userID)
        {
            var userExists = await userRepository.IsUserExistAsync(userID);

            if (!userExists)
            {
                throw new NotFoundException($"User doesn't exist with ID {userID}");
            }

            var hishabs = await hishabRepository.GetHishabsByUserAsync(userID);

            if (hishabs is null)
            {
                return new List<HishabResponseDTO>();
            }

            List<HishabResponseDTO> hishabResponseDto = hishabs.Select(_hishab => new HishabResponseDTO
            {
                HishabID = _hishab.HishabID,
                UserID = _hishab.UserID,
                Name = _hishab.Name,
                Amount = _hishab.Amount,
                CardColor = _hishab.CardColor,
                TransactionHistories = _hishab.Histories?.Select(_history => new HistoryResponseDTO
                {
                    HistoryName = _history.HistoryName,
                    HistoryType = _history.HistoryType,
                    CreatedDate = _history.CreatedDate.ToString()
                }).ToList()
            }).ToList();

            return hishabResponseDto;
        }

        public async Task<HishabResponseDTO?> GetHishabByIDAsync(int hishabID)
        {
            var _hishab = await hishabRepository.GetHishabByIDAsync(hishabID);

            if (_hishab is null)
            {
                throw new NotFoundException($"Hishab doesn't exist with ID {hishabID}");
            }

            HishabResponseDTO userResponseDto = new HishabResponseDTO
            {
                HishabID = _hishab.HishabID,
                UserID = _hishab.UserID,
                Name = _hishab.Name,
                Amount = _hishab.Amount,
                CardColor = _hishab.CardColor,
                TransactionHistories = _hishab.Histories?.Select(_history => new HistoryResponseDTO
                {
                    HistoryName = _history.HistoryName,
                    HistoryType = _history.HistoryType,
                    CreatedDate = _history.CreatedDate.ToString()
                }).ToList()
            };

            return userResponseDto;
        }

        public async Task<HishabAmountUpdateResponseDTO?> IncreaseAmountAsync(HishabAmountUpdateRequestDTO hishabAmountUpdateRequestDto)
        {
            if (hishabAmountUpdateRequestDto is null)
            {
                throw new InvalidInputException("Input format is not valid");
            }

            if (hishabAmountUpdateRequestDto.Amount <= 0)
            {
                throw new InvalidInputException("Amount can't be a negative value");
            }

            var updatedHishab = await hishabRepository.IncreaseAmountAsync(
                hishabAmountUpdateRequestDto.HishabID,
                hishabAmountUpdateRequestDto.Amount);

            if (updatedHishab is null)
            {
                throw new NotFoundException($"Hishab doesn't exist with ID {hishabAmountUpdateRequestDto.HishabID}");
            }

            var hishabUpdateResponseDto = new HishabAmountUpdateResponseDTO
            {
                HishabID = updatedHishab.HishabID,
                UserID = updatedHishab.UserID,
                Name = updatedHishab.Name,
                UpdatedAmount = updatedHishab.Amount
            };

            return hishabUpdateResponseDto;
        }

        public async Task<HishabAmountUpdateResponseDTO?> DecreaseAmountAsync(HishabAmountUpdateRequestDTO hishabAmountUpdateRequestDto)
        {
            if (hishabAmountUpdateRequestDto is null)
            {
                throw new InvalidInputException("Input format is not valid");
            }

            if (hishabAmountUpdateRequestDto.Amount <= 0)
            {
                throw new InvalidInputException("Amount can't be a negative value");
            }

            var updatedHishab = await hishabRepository.DecreaseAmountAsync(
                hishabAmountUpdateRequestDto.HishabID,
                hishabAmountUpdateRequestDto.Amount);

            if (updatedHishab is null)
            {
                throw new NotFoundException($"Hishab doesn't exist with ID {hishabAmountUpdateRequestDto.HishabID}");
            }

            var hishabUpdateResponseDto = new HishabAmountUpdateResponseDTO
            {
                HishabID = updatedHishab.HishabID,
                UserID = updatedHishab.UserID,
                Name = updatedHishab.Name,
                UpdatedAmount = updatedHishab.Amount
            };

            return hishabUpdateResponseDto;
        }

        public async Task<EditHishabDTO?> EditHishabAsync(EditHishabDTO editHishabDto)
        {
            if(editHishabDto is null)
            {
                throw new InvalidInputException("Input format is not valid");
            }

            var hishab = await hishabRepository.EditHishabAsync(editHishabDto.HishabID,
                editHishabDto.UpdatedHishabName,
                editHishabDto.UpdatedCardColor);

            if(hishab is null)
            {
                throw new NotFoundException($"Hishab doesn't exist with ID {editHishabDto.HishabID}");
            }

            var updatedHishabDto = new EditHishabDTO
            {
                HishabID = hishab.HishabID,
                UpdatedHishabName = hishab.Name,
                UpdatedCardColor = hishab.CardColor
            };

            return updatedHishabDto;
        }

        public async Task<bool> DeleteHishabAsync(int hishabID)
        {
            return await hishabRepository.DeleteHishabAsync(hishabID);
        }
    }
}

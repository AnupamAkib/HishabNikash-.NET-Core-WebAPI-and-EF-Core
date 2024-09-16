using HishabNikash.Models;

namespace HishabNikash.Repositories
{
    public interface IHishabRepository
    {
        Task<Hishab> CreateNewHishabAsync(Hishab hishab);
        Task<List<Hishab>> GetHishabsByUserAsync(int userId);
        Task<Hishab> GetHishabByIDAsync(int hishabID);
        Task<Hishab> IncreaseAmountAsync(int hishabID, int amount);
        Task<Hishab> DecreaseAmountAsync(int hishabID, int amount);
        Task<Hishab> EditHishabAsync(int hishabID, string updatedHishabName, string updatedCardColor);
        Task<bool> DeleteHishabAsync(int hishabID);
    }
}

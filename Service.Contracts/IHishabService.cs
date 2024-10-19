using HishabNikash.Models;

namespace Service.Contracts
{
    public interface IHishabService
    {
        Task<Hishab> CreateNewHishab(Hishab hishab);
        Task<IEnumerable<Hishab>?> GetHishabsByUserAsync(int userID);
        Task<Hishab?> GetHishabByIDAsync(int hishabID);
        bool IncreaseAmount(int hishabID, int amount);
        bool DecreaseAmount(int hishabID, int amount);
        Task<Hishab?> EditHishab(int hishabID, string updatedHishabName, string updatedCardColor);
        Task<bool> DeleteHishab(int hishabID);
    }
}

using HishabNikash.Models;

namespace Contracts
{
    public interface IHishabRepository
    {
        void CreateNewHishab(Hishab hishab);
        Task<List<Hishab>?> GetHishabsByUserAsync(int userID);
        Task<Hishab?> GetHishabByIDAsync(int hishabID);
        bool IncreaseAmount(int hishabID, int amount);
        bool DecreaseAmount(int hishabID, int amount);
        Hishab? EditHishab(int hishabID, string updatedHishabName, string updatedCardColor);
        bool DeleteHishab(int hishabID);
    }
}

using Contracts;
using HishabNikash.Models;
using Service.Contracts;

namespace Service
{
    internal sealed class HishabService : IHishabService
    {
        private readonly IRepositoryManager repositoryManager;

        public HishabService(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public async Task<Hishab> CreateNewHishab(Hishab hishab)
        {
            var _hishab = await repositoryManager.Hishab.CreateNewHishab(hishab);
            repositoryManager.Save();
            return _hishab;
        }

        public bool DecreaseAmount(int hishabID, int amount)
        {
            bool result = repositoryManager.Hishab.DecreaseAmount(hishabID, amount);
            repositoryManager.Save();
            return result;
        }

        public async Task<bool> DeleteHishab(int hishabID)
        {
            return await repositoryManager.Hishab.DeleteHishab(hishabID);
        }

        public async Task<Hishab?> EditHishab(int hishabID, string updatedHishabName, string updatedCardColor)
        {
            var hishab = await repositoryManager.Hishab.EditHishab(hishabID, updatedHishabName, updatedCardColor);
            repositoryManager.Save();
            return hishab;
        }

        public async Task<Hishab?> GetHishabByIDAsync(int hishabID)
        {
            return await repositoryManager.Hishab.GetHishabByIDAsync(hishabID);
        }

        public async Task<IEnumerable<Hishab>?> GetHishabsByUserAsync(int userID)
        {
            return await repositoryManager.Hishab.GetHishabsByUserAsync(userID);
        }

        public bool IncreaseAmount(int hishabID, int amount)
        {
            bool result = repositoryManager.Hishab.IncreaseAmount(hishabID, amount);
            repositoryManager.Save();
            return result;
        }
    }
}

using HishabNikash.Context;
using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;

namespace HishabNikash.Repositories
{
    public class HishabRepository : IHishabRepository
    {
        private ApplicationDBContext dbContext;

        public HishabRepository(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Hishab> CreateNewHishabAsync(Hishab hishab)
        {
            dbContext.Hishabs.Add(hishab);
            await dbContext.SaveChangesAsync();
            return hishab;
        }

        public async Task<Hishab> GetHishabByIDAsync(int hishabID)
        {
            var hishab = await dbContext.Hishabs
                    .Where(hishab => hishab.HishabID == hishabID)
                    .Include(h => h.Histories)
                    .FirstOrDefaultAsync();
            return hishab;
        }

        public async Task<List<Hishab>> GetHishabsByUserAsync(int userID)
        {
            var hishabs = await dbContext.Hishabs
                    .Where(hishab => hishab.UserID == userID)
                    .Include(h => h.Histories)
                    .ToListAsync();
            return hishabs;
        }

        public async Task<Hishab?> IncreaseAmountAsync(int hishabID, int amount)
        {
            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == hishabID);
            if ((hishab is null))
            {
                return null;
            }

            hishab.Amount += amount;

            var history = new History
            {
                HistoryName = $"{amount} taka has been credited",
                HistoryType = "credited"
            };

            hishab.Histories ??= new List<History>();
            hishab.Histories.Add(history);
            await dbContext.SaveChangesAsync();
            return hishab;
        }

        public async Task<Hishab> DecreaseAmountAsync(int hishabID, int amount)
        {
            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == hishabID);
            if ((hishab is null))
            {
                return null;
            }

            hishab.Amount -= amount;

            var history = new History
            {
                HistoryName = $"{amount} taka has been credited",
                HistoryType = "credited"
            };

            hishab.Histories ??= new List<History>();
            hishab.Histories.Add(history);
            await dbContext.SaveChangesAsync();
            return hishab;
        }

        public async Task<bool> DeleteHishabAsync(int hishabID)
        {
            var hishab = await dbContext.Hishabs.FirstOrDefaultAsync(h => h.HishabID == hishabID);
            if (hishab is not null)
            {
                dbContext.Hishabs.Remove(hishab);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Hishab?> EditHishabAsync(int hishabID, string updatedHishabName, string updatedCardColor)
        {
            var hishab = dbContext.Hishabs.FirstOrDefault(h => h.HishabID == hishabID);
            if (hishab is null)
            {
                return null;
            }

            hishab.Name = updatedHishabName;
            hishab.CardColor = updatedCardColor;
            await dbContext.SaveChangesAsync();
            return hishab;
        }
    }
}

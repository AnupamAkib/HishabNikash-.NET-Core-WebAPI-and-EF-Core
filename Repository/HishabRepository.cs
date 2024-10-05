using Contracts;
using HishabNikash.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class HishabRepository : RepositoryBase<Hishab>, IHishabRepository
    {
        public HishabRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNewHishab(Hishab hishab)
        {
            Create(hishab);
        }

        public bool DeleteHishab(int hishabID)
        {
            var hishab = FindByCondition(h => h.ID == hishabID).FirstOrDefault();

            if (hishab is not null)
            {
                Delete(hishab);
                return true;
            }
            return false;
        }

        public Hishab? EditHishab(int hishabID, string updatedHishabName, string updatedCardColor)
        {
            var hishab = FindByCondition(h => h.ID == hishabID).FirstOrDefault();

            if (hishab is null)
            {
                return null;
            }

            //save history for color changing as well
            var history = new History
            {
                HistoryName = $"Hishab renamed from {hishab.Name} to {updatedHishabName}",
                HistoryType = "others"
            };

            hishab.Name = updatedHishabName;
            hishab.CardColor = updatedCardColor;
            hishab.Histories?.Add(history);

            Update(hishab);

            return hishab;
        }

        public async Task<Hishab?> GetHishabByIDAsync(int hishabID)
        {
            return await FindByCondition(h => h.ID == hishabID).FirstOrDefaultAsync();
        }

        public async Task<List<Hishab>?> GetHishabsByUserAsync(int userID)
        {
            return await FindByCondition(u => u.ID == userID).ToListAsync();
        }

        public bool IncreaseAmount(int hishabID, int amount)
        {
            var hishab = FindByCondition(h => h.ID == hishabID).FirstOrDefault();

            if ((hishab is null))
            {
                return false;
            }

            hishab.Amount += amount;

            var history = new History
            {
                HistoryName = $"{amount} taka has been credited",
                HistoryType = "credited"
            };

            hishab.Histories ??= new List<History>();
            hishab.Histories.Add(history);

            Update(hishab);

            return true;
        }

        public bool DecreaseAmount(int hishabID, int amount)
        {
            var hishab = FindByCondition(h => h.ID == hishabID).FirstOrDefault();

            if ((hishab is null))
            {
                return false;
            }

            hishab.Amount -= amount;

            var history = new History
            {
                HistoryName = $"{amount} taka has been debited",
                HistoryType = "debited"
            };

            hishab.Histories ??= new List<History>();
            hishab.Histories.Add(history);

            Update(hishab);

            return true;
        }
    }
}

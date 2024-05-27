using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services
{
    public class LossReasonService : ILossReason
    {
        private readonly IdentityDbContext _identityDbContext;

        public LossReasonService (IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task<List<LossReasonViewModel>> GetAllAsync()
        {
            var lossReasonList = await _identityDbContext.LossReasons.ToListAsync();

            var lossReasonViewModelList = lossReasonList.Select(lossReason => new LossReasonViewModel
            {
                Id = lossReason.Id,
                Reason = lossReason.Reason
            }).ToList();

            return lossReasonViewModelList;
        }

        public async Task<LossReasonViewModel> GetByIdAsync(int id)
        {
            var lossReason = await _identityDbContext.LossReasons.FindAsync(id);

            if (lossReason == null)
            {
                return null;
            }

            var lossReasonViewModel = new LossReasonViewModel
            {
                Id = lossReason.Id,
                Reason = lossReason.Reason
            };

            return lossReasonViewModel;
        }

        public async Task<LossReasonViewModel> CreateAsync(LossReasonViewModel lossReason)
        {
            var newLossReason = new LossReason
            {
                Reason = lossReason.Reason
            };

            _identityDbContext.LossReasons.Add(newLossReason);
            await _identityDbContext.SaveChangesAsync();

            return lossReason;
        }

        public async Task<bool> UpdateAsync(int id, LossReasonViewModel lossReason)
        {
            var existingLossReason = await _identityDbContext.LossReasons.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (existingLossReason != null)
            {
                existingLossReason.Reason = lossReason.Reason;

                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lossReason = await _identityDbContext.LossReasons.Where(f => f.Id == id).FirstOrDefaultAsync();

            if (lossReason != null)
            {
                _identityDbContext.LossReasons.Remove(lossReason);
                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<LossReasonViewModel>> SearchAsync(string lossReason)
        {
            var lossReasonList = await _identityDbContext.LossReasons
                .Where(reason => reason.Reason.Contains(lossReason))
                .ToListAsync();

            var lossReasonViewModelList = lossReasonList.Select(lossReason => new LossReasonViewModel
            {
                Id = lossReason.Id,
                Reason = lossReason.Reason
            }).ToList();

            return lossReasonViewModelList;
        }
    }
}

using EccomerceApi.Data;
using EccomerceApi.Interfaces;
using EccomerceApi.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services
{
    public class LossService : ILoss
    {
        private readonly IdentityDbContext _identityDbContext;

        public LossService(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<List<LossesViewModel>> GetAllAsync()
        {
            var lossList = await _identityDbContext.Losses
                .Include(l => l.LostDetails)
                    .ThenInclude(ld => ld.Product)
                .Include(l => l.LostDetails)
                    .ThenInclude(ld => ld.LossReason)
                .OrderByDescending(l => l.Date) // Ordenar por fecha más reciente
                .ToListAsync();

            var lossViewModelList = lossList.Select(loss => new LossesViewModel
            {
                Id = loss.Id,
                Date = loss.Date,
                Total = loss.Total,
                ProductId = loss.LostDetails.FirstOrDefault()?.ProductId ?? 0,
                ProductName = loss.LostDetails.FirstOrDefault()?.Product?.Name,
                Amount = loss.LostDetails.FirstOrDefault()?.Amount,
                UnitPrice = loss.LostDetails.FirstOrDefault()?.UnitCost,
                Reason = loss.LostDetails.FirstOrDefault()?.LossReason?.reason
            }).ToList();

            return lossViewModelList;
        }

        public async Task<List<LossesViewModel>> SearchAsync(string name)
        {
            var matchedLosses = await _identityDbContext.Losses
                .Include(l => l.LostDetails)
                    .ThenInclude(ld => ld.Product)
                .Include(l => l.LostDetails)
                    .ThenInclude(ld => ld.LossReason)
                .Where(l => l.LostDetails.Any(ld => ld.Product.Name.Contains(name)))
                .OrderByDescending(l => l.Date) // Ordenar por fecha más reciente
                .ToListAsync();

            var lossViewModelList = matchedLosses.Select(loss => new LossesViewModel
            {
                Id = loss.Id,
                Date = loss.Date,
                Total = loss.Total,
                ProductId = loss.LostDetails.FirstOrDefault()?.ProductId ?? 0,
                ProductName = loss.LostDetails.FirstOrDefault()?.Product?.Name,
                Amount = loss.LostDetails.FirstOrDefault()?.Amount,
                UnitPrice = loss.LostDetails.FirstOrDefault()?.UnitCost,
                Reason = loss.LostDetails.FirstOrDefault()?.LossReason?.reason
            }).ToList();

            return lossViewModelList;
        }

    }
}

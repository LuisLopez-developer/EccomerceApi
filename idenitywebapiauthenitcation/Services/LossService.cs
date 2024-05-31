using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Model.CreateModel;
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

        public async Task<LossCreateModel> CreateAsync(LossCreateModel lossCreateModel)
        {

            if (lossCreateModel.Date == null)
            {
                var peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                var currentTimePeru = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);
                lossCreateModel.Date = currentTimePeru;
            }

            var newLoss = new Loss
            {
                Date = lossCreateModel.Date,
                Total = lossCreateModel.Total,
                StateId = lossCreateModel.StateId,
                LostDetails = new List<LostDetail>
                {
                    new LostDetail
                    {
                        UnitCost = lossCreateModel.UnitCost,
                        Amount = lossCreateModel.Amount,
                        ProductId = lossCreateModel.ProductId,
                        Description = lossCreateModel.Description,
                        LossReasonId = lossCreateModel.LossReasonId
                    }
                }
            };

            _identityDbContext.Losses.Add(newLoss);
            await _identityDbContext.SaveChangesAsync();

            // Actualizar la existencia del producto correspondiente
            var product = await _identityDbContext.Products.FindAsync(lossCreateModel.ProductId);
            if (product != null)
            {
                product.Existence -= lossCreateModel.Amount; // Restar la cantidad perdida a la existencia
                await _identityDbContext.SaveChangesAsync();
            }

            return lossCreateModel;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var loss = await _identityDbContext.Losses.FindAsync(id);

            if (loss != null)
            {
                _identityDbContext.Losses.Remove(loss);
                await _identityDbContext.SaveChangesAsync();
                return true;
            }

            return false;
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
                Reason = loss.LostDetails.FirstOrDefault()?.LossReason?.Reason
            }).ToList();

            return lossViewModelList;
        }

        public async Task<LossCreateModel> GetByIdAsync(int id)
        {
            var loss = await _identityDbContext.Losses
                .Include(l => l.LostDetails)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (loss == null)
            {
                return null;
            }

            var lossDetail = loss.LostDetails.FirstOrDefault();

            var lossCreateModel = new LossCreateModel
            {
                Id = loss.Id,
                Date = loss.Date,
                Total = loss.Total,
                StateId = loss.StateId,
                UnitCost = loss.LostDetails.FirstOrDefault()?.UnitCost,
                Amount = loss.LostDetails.FirstOrDefault().Amount,
                Description = loss.LostDetails.FirstOrDefault()?.Description,
                ProductId = loss.LostDetails.FirstOrDefault()?.ProductId,
                LossReasonId = loss.LostDetails.FirstOrDefault()?.LossReasonId
            };

            return lossCreateModel;
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
                Reason = loss.LostDetails.FirstOrDefault()?.LossReason?.Reason
            }).ToList();

            return lossViewModelList;
        }

        public async Task<bool> UpdateAsync(int id, LossCreateModel lossCreateModel)
        {
            var loss = await _identityDbContext.Losses
            .Include(l => l.LostDetails)
            .FirstOrDefaultAsync(l => l.Id == id);

            if (loss == null)
            {
                return false; // No se encontró la pérdida
            }

            // Actualizar los detalles de la pérdida
            loss.Date = lossCreateModel.Date ?? loss.Date;
            loss.Total = lossCreateModel.Total;
            loss.StateId = lossCreateModel.StateId;

            var lossDetail = loss.LostDetails.FirstOrDefault();
            if (lossDetail != null)
            {
                var oldAmount = lossDetail.Amount;

                lossDetail.UnitCost = lossCreateModel.UnitCost;
                lossDetail.Amount = lossCreateModel.Amount;
                lossDetail.ProductId = lossCreateModel.ProductId;
                lossDetail.Description = lossCreateModel.Description;
                lossDetail.LossReasonId = lossCreateModel.LossReasonId;

                // Actualizar la existencia del producto correspondiente
                var product = await _identityDbContext.Products.FindAsync(lossCreateModel.ProductId);
                if (product != null)
                {
                    product.Existence += oldAmount; // Revertir la cantidad antigua
                    product.Existence -= lossCreateModel.Amount; // Restar la nueva cantidad
                }
            }

            await _identityDbContext.SaveChangesAsync();
            return true;
        }
    }
}

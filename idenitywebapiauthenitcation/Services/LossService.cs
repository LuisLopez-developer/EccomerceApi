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
            var loss = await _identityDbContext.Losses.FindAsync(id);

            if (loss == null)
            {
                return null;
            }

            var lossCreateModel = new LossCreateModel
            {
                Id =loss.Id,
                Date = loss.Date,
                Total = loss.Total,
                StateId = loss.StateId,
                UnitCost = loss.LostDetails?.FirstOrDefault()?.UnitCost,
                Amount = loss.LostDetails?.FirstOrDefault()?.Amount,
                Description = loss.LostDetails.FirstOrDefault().Description,
                ProductId = loss.LostDetails?.FirstOrDefault()?.ProductId,
                LossReasonId = loss.LostDetails?.FirstOrDefault()?.LossReasonId
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
            var existingLoss = await _identityDbContext.Losses
                    .Include(l => l.LostDetails)
                    .FirstOrDefaultAsync(l => l.Id == id);

            if (existingLoss == null)
            {
                return false;
            }

            // Actualizar las propiedades de la pérdida
            existingLoss.Date = lossCreateModel.Date ?? existingLoss.Date;
            existingLoss.Total = lossCreateModel.Total ?? existingLoss.Total;
            existingLoss.StateId = lossCreateModel.StateId ?? existingLoss.StateId;

            // Actualizar los detalles de la pérdida
            if (existingLoss.LostDetails != null && existingLoss.LostDetails.Any())
            {
                var existingDetail = existingLoss.LostDetails.FirstOrDefault();
                if (existingDetail != null)
                {
                    existingDetail.UnitCost = lossCreateModel.UnitCost ?? existingDetail.UnitCost;
                    existingDetail.Amount = lossCreateModel.Amount ?? existingDetail.Amount;
                    existingDetail.ProductId = lossCreateModel.ProductId ?? existingDetail.ProductId;
                    existingDetail.Description = lossCreateModel.Description ?? existingDetail.Description;
                    existingDetail.LossReasonId = lossCreateModel.LossReasonId ?? existingDetail.LossReasonId;
                }
            }

            _identityDbContext.Losses.Update(existingLoss);
            await _identityDbContext.SaveChangesAsync();

            // Actualizar la existencia del producto correspondiente
            var product = await _identityDbContext.Products.FindAsync(lossCreateModel.ProductId);
            if (product != null)
            {
                // Asumiendo que la existencia del producto se actualiza según la diferencia entre la cantidad original y la nueva cantidad
                var existingDetail = existingLoss.LostDetails.FirstOrDefault();
                if (existingDetail != null)
                {
                    var difference = lossCreateModel.Amount - existingDetail.Amount;
                    product.Existence -= difference;
                }
                await _identityDbContext.SaveChangesAsync();
            }

            return true;
        }
    }
}

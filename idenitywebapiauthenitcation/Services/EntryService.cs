using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services
{
    public class EntryService : IEntry
    {
        private readonly IdentityDbContext _identityDbContext;
        public EntryService(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task<List<EntryViewModel>> GetAllAsync()
        {
            var entryList = await _identityDbContext.Entries
            .Include(e => e.EntryDetails) // Incluye las propiedades de navegación de Entry
                .ThenInclude(ed => ed.Product) // Incluye la entidad Product relacionada con EntryDetail
            .ToListAsync();

                var entryViewModelList = entryList.SelectMany(entry => entry.EntryDetails.Select(entryDetail => new EntryViewModel
                {
                    IdEntry = entry.Id,
                    Date = entry.Date,
                    Name = entryDetail.Product?.Name,
                    UnitCost = entryDetail.UnitCost,
                    Amount = entryDetail.Amount,
                    Total = entry.Total,
                    Existence = entryDetail.Product?.Existence
                })).ToList();

                return entryViewModelList;
        }

        public async Task<EntryCreateModel> CreateAsync(EntryCreateModel entryCreateModel)
        {
            if (entryCreateModel.Date == null)
            {
                var peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                var currentTimePeru = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);
                entryCreateModel.Date = currentTimePeru;
            }

            var newEntry = new Entry
            {
                Date = entryCreateModel.Date,
                Total = entryCreateModel.Total,
                StateId = entryCreateModel.IdState,
                EntryDetails = new List<EntryDetail>
                {
                    new EntryDetail
                    {
                        UnitCost = entryCreateModel.UnitCost,
                        Amount = entryCreateModel.Amount,
                        ProductId = entryCreateModel.IdProduct
                    }
                }
            };

            _identityDbContext.Entries.Add(newEntry);
            await _identityDbContext.SaveChangesAsync();

            return entryCreateModel;
        }
    }
    
}

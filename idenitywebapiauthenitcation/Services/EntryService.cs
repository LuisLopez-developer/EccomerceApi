using EccomerceApi.Data;
using EccomerceApi.Interfaces;
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
                .ThenInclude(ed => ed.IdProductNavigation) // Incluye la entidad Product relacionada con EntryDetail
                .ToListAsync();

            var entryViewModelList = entryList.SelectMany(entry => entry.EntryDetails.Select(entryDetail => new EntryViewModel
            {
                IdEntry = entry.Id,
                Date = entry.Date,
                Name = entryDetail.IdProductNavigation?.Name,
                UnitCost = entryDetail.UnitCost,
                Amount = entryDetail.Amount,
                Total = entry.Total,
                Existence = entryDetail.IdProductNavigation?.Existence
            })).ToList();

            return entryViewModelList;
        }
    }
}

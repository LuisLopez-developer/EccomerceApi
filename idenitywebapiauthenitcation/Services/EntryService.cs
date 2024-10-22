using Data;
using EccomerceApi.Herlpers;
using EccomerceApi.Interfaces;
using EccomerceApi.Model;
using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EccomerceApi.Services
{
    public class EntryService : IEntry
    {
        private readonly AppDbContext _identityDbContext;

        private readonly IBatch _batchService;

        public EntryService(AppDbContext identityDbContext, IBatch batchService)
        {
            _identityDbContext = identityDbContext;
            _batchService = batchService;
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
            try
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
                };

                _identityDbContext.Entries.Add(newEntry);
                await _identityDbContext.SaveChangesAsync();
                Console.WriteLine("Entrada creada con éxito" + newEntry.Id);

                var batchModel = new BatchModel
                {
                    InitialQuantity = entryCreateModel.Amount ?? 0,
                    RemainingQuantity = entryCreateModel.Amount ?? 0,
                    EntryDate = entryCreateModel.Date ?? getTimePeruHelper.GetCurrentTimeInPeru(),
                    Cost = entryCreateModel.UnitCost ?? 0,
                    ProductId = entryCreateModel.IdProduct ?? 0
                };

                batchModel = await _batchService.CreateAsync(batchModel);
                Console.WriteLine("Entrada creada con éxito" + batchModel.Id);


                newEntry.EntryDetails = new List<EntryDetail>
                {
                    new EntryDetail
                    {
                        BatchId = batchModel.Id,
                        EntryId = newEntry.Id,
                        ProductId = entryCreateModel.IdProduct,
                        UnitCost = entryCreateModel.UnitCost,
                        Amount = entryCreateModel.Amount
                    }
                };

                _identityDbContext.EntryDetails.Add(newEntry.EntryDetails.First());
                await _identityDbContext.SaveChangesAsync();

                return entryCreateModel;
            }
            catch (Exception ex)
            {
                // Acceder a la excepción interna
                var innerExceptionMessage = ex.InnerException?.Message ?? "No hay excepción interna";
                // Registrar el mensaje de la excepción interna
                Console.WriteLine("Excepción interna: " + innerExceptionMessage);

                throw new InvalidOperationException("Error al crear la entrada: " + ex.Message, ex);
            }
        }

        public async Task<List<EntryViewModel>> FilterByDateAsync(DateTime startDate, DateTime endDate)
        {
            var entryList = await _identityDbContext.Entries
                .Include(e => e.EntryDetails)
                .ThenInclude(ed => ed.Product)
                .Where(e => e.Date >= startDate && e.Date <= endDate)
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
    }

}

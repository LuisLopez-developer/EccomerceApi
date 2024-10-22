using Data;
using EccomerceApi.Interfaces;
using EccomerceApi.Model;
using Models;

namespace EccomerceApi.Services
{
    public class BatchService : IBatch
    {
        private readonly AppDbContext _identityDbContext;

        public BatchService(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<Model.BatchViewModel> CreateAsync(Model.BatchViewModel batchModel)
        {
            try
            {
                var batch = new Models.BatchModel
                {
                    InitialQuantity = batchModel.InitialQuantity,
                    RemainingQuantity = batchModel.RemainingQuantity,
                    EntryDate = batchModel.EntryDate,
                    Cost = batchModel.Cost,
                    ProductId = batchModel.ProductId
                };

                _identityDbContext.Batches.Add(batch);
                await _identityDbContext.SaveChangesAsync();

                return new Model.BatchViewModel
                {
                    Id = batch.Id,
                    InitialQuantity = batch.InitialQuantity,
                    RemainingQuantity = batch.RemainingQuantity,
                    EntryDate = batch.EntryDate,
                    Cost = batch.Cost,
                    ProductId = batch.ProductId
                };

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear el lote: " + ex.Message, ex);
            }
        }
    }
}

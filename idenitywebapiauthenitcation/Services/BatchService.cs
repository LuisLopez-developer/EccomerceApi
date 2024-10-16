using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Model;

namespace EccomerceApi.Services
{
    public class BatchService : IBatch
    {
        private readonly IdentityDbContext _identityDbContext;

        public BatchService(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<BatchModel> CreateAsync(BatchModel batchModel)
        {
            try
            {
                var batch = new Batch
                {
                    InitialQuantity = batchModel.InitialQuantity,
                    RemainingQuantity = batchModel.RemainingQuantity,
                    EntryDate = batchModel.EntryDate,
                    Cost = batchModel.Cost,
                    ProductId = batchModel.ProductId
                };

                _identityDbContext.Batches.Add(batch);
                await _identityDbContext.SaveChangesAsync();

                return new BatchModel
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

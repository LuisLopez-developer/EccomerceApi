using EccomerceApi.Model;

namespace EccomerceApi.Interfaces
{
    public interface IBatch
    {
        Task<BatchModel> CreateAsync(BatchModel batchModel);
    }
}

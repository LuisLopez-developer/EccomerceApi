using EccomerceApi.Model;

namespace EccomerceApi.Interfaces
{
    public interface IBatch
    {
        Task<BatchViewModel> CreateAsync(BatchViewModel batchModel);
    }
}

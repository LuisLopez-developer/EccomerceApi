using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;

namespace EccomerceApi.Interfaces
{
    public interface ILoss
    {
        Task<List<LossesViewModel>> GetAllAsync();
        Task<List<LossesViewModel>> SearchAsync(string name);
        Task<LossCreateModel> GetByIdAsync(int id);
        Task<LossCreateModel> CreateAsync(LossCreateModel lossCreateModel);
        Task<bool> UpdateAsync(int id, LossCreateModel lossCreateModel);
        Task<bool> DeleteAsync(int id);
    }
}

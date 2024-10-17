using EccomerceApi.Model.ViewModel;

namespace EccomerceApi.Interfaces
{
    public interface ILossReason
    {
        Task<List<LossReasonViewModel>> GetAllAsync();
        Task<List<LossReasonViewModel>> SearchAsync(string name);
        Task<LossReasonViewModel> GetByIdAsync(int id);
        Task<LossReasonViewModel> CreateAsync(LossReasonViewModel lossReason);
        Task<bool> UpdateAsync(int id, LossReasonViewModel lossReason);
        Task<bool> DeleteAsync(int id);
    }
}

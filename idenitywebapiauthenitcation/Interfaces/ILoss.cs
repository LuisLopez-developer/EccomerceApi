using EccomerceApi.Model.ViewModel;

namespace EccomerceApi.Interfaces
{
    public interface ILoss
    {
        Task<List<LossesViewModel>> GetAllAsync();
        Task<List<LossesViewModel>> SearchAsync(string name);
    }
}

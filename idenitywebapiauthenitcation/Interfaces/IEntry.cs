using EccomerceApi.Model.ViewModel;

namespace EccomerceApi.Interfaces
{
    public interface IEntry
    {
        Task<List<EntryViewModel>> GetAllAsync();
    }
}

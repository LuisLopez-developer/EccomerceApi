using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;

namespace EccomerceApi.Interfaces
{
    public interface IEntry
    {
        Task<List<EntryViewModel>> GetAllAsync();

        Task<EntryCreateModel> CreateAsync(EntryCreateModel product);

        Task<List<EntryViewModel>> FilterByDateAsync(DateTime startDate, DateTime endDate);
    }
}

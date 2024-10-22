using Models;

namespace EccomerceApi.Interfaces
{
    public interface IState
    {
        Task<List<State>> GetAllAsync();
    }
}

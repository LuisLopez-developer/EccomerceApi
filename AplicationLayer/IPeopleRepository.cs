using EnterpriseLayer;

namespace AplicationLayer
{
    public interface IPeopleRepository
    {
        Task<int> AddAsync(People entity);
        Task<bool> ExistByDNIAsync(string dni);
        Task<int> GetIdByDNIAsync(string dni);
    }
}

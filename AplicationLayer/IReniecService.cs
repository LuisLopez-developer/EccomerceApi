using EnterpriseLayer;

namespace AplicationLayer
{
    public interface IReniecService
    {
        Task<People> GetPersonDataByDNIAsync(string dni);
    }
}

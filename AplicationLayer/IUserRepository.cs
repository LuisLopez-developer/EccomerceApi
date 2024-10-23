using EnterpriseLayer;

namespace AplicationLayer
{
    public interface IUserRepository
    {
        Task<string> GetDNIByUserID(string userId);

        Task<User> GetUserById(string id);
    }
}

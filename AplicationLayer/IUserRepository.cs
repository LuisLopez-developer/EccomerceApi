using EnterpriseLayer;

namespace AplicationLayer
{
    public interface IUserRepository
    {
        Task<string> GetDNIByUserID(string userId);

        Task<User> GetUserById(string id);
        Task<bool> IsUserLinkedToPersonAsync(string userId);
        Task LinkUserToPersonAsync(string userId, int personId);
    }
}

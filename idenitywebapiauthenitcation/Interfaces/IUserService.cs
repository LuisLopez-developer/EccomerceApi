using EccomerceApi.Model;
using Models;

namespace EccomerceApi.Interfaces
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUsers();

        Task<UserViewModel> GetUserByEmail(string emailId);

        Task<bool> UpdateUser(string emailId, UserViewModel user);

        Task<bool> DeleteUserByEmail(string emailId);

        Task<bool> BlockUserAsync(string emailId)
    }
}

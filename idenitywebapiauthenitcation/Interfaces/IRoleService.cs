using EccomerceApi.Model;

namespace EccomerceApi.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetRolesAsync();

        Task<List<string>> GetUserRolesAsync(string emailId);

        Task<List<string>> AddRolesAsync(string[] roles);

        Task<bool> AddUserRoleAsync(string userEmail, string[] roles);

    }
}

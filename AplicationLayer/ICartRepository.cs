using EnterpriseLayer;

namespace AplicationLayer
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetByUserIdAsync(string userId);
        Task<int> GetTotalProductQuantityByUserIdAsync(string userId);
        Task<bool> UserHasCartAsync(string userId);
        Task ChangeItemQuantityAsync(int itemId, int quantity);
    }
}

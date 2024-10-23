using EnterpriseLayer;

namespace AplicationLayer
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetByUserIdAsync(string userId);
    }
}

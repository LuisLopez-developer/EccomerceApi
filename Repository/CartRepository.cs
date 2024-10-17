using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class CartRepository : IRepository<Cart>
    {

        private readonly AppDbContext _dbContext;

        public CartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Cart entity)
        {
            var cartModel = new CartModel
            {
                ProductId = entity.ProductId,
                UserId = entity.UserId,
                Quantity = entity.Quantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _dbContext.Carts.AddAsync(cartModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cart>> GetAllAsync() => await _dbContext.Carts.Select(c => new Cart
        {
            Id = c.Id,
            ProductId = c.ProductId,
            UserId = c.UserId,
            Quantity = c.Quantity
        }).ToListAsync();

        public async Task<Cart> GetById(int id)
        {
            var cart = await _dbContext.Carts.FindAsync(id);
            return new Cart
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                UserId = cart.UserId,
                Quantity = cart.Quantity
            };
        }
    }
}

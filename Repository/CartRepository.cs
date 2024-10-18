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

        public async Task AddAsync(Cart cart)
        {
            var cartModel = new CartModel();
            cartModel.UserId = cart.UserId;
            cartModel.CreatedAt = cart.CreatedAt;
            cartModel.CartItems = cart.CartItems.Select(ci => new CartItemModel
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                CreatedAt = ci.CreatedAt

            }).ToList();

            await _dbContext.Carts.AddAsync(cartModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cart>> GetAllAsync() 
            => await _dbContext.Carts
                .Select(c => new Cart(c.Id, c.UserId, c.CreatedAt, 
                                        _dbContext.CartItems
                                            .Where(ci => ci.CartId == c.Id)
                                            .Select(ci => new CartItem(ci.ProductId, ci.Quantity, ci.CreatedAt))
                                            .ToList()
                                    )
                ).ToListAsync();

        public async Task<Cart> GetById(int id)
        {
            var cartModel = await _dbContext.Carts.FindAsync(id);
            return new Cart(cartModel.Id, cartModel.UserId, cartModel.CreatedAt,
                                _dbContext.CartItems
                                    .Where(ci => ci.CartId == cartModel.Id)
                                    .Select(ci => new CartItem(ci.ProductId, ci.Quantity, ci.CreatedAt))
                                    .ToList()
                            );
        }
    }
}

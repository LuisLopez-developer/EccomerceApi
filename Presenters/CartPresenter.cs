using AplicationLayer;
using EnterpriseLayer;

namespace Presenters
{
    public class CartPresenter : IPresenter<Cart, CartViewModel>
    {
        public IEnumerable<CartViewModel> Present(IEnumerable<Cart> carts)
            => carts.Select(c => new CartViewModel
            {
                Id = c.Id,
                Quantity = c.Quantity,
                ProductId = c.ProductId,
                UserId = c.UserId
            });
    }
}

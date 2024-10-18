using AplicationLayer;
using EnterpriseLayer;

namespace Presenters.SaleViewModel
{
    public class CartDetailPresenter : IPresenter<Cart, CartDetailViewModel>
    {
        public IEnumerable<CartDetailViewModel> Present(IEnumerable<Cart> carts)
            => carts.Select(cart => new CartDetailViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(cartItem => new CartItemViewModel
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                }).ToList()
            });
    }
}

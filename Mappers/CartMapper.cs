using AplicationLayer;
using EnterpriseLayer;
using Mappers.Dtos.Requests;

namespace Mappers
{
    public class CartMapper : IMapper<CartRequestDTO, Cart>
    {
        public Cart ToEntity(CartRequestDTO dto)
        {
            var cartItems = new List<CartItem>();

            foreach (var item in dto.CartItems)
            {
                cartItems.Add(new CartItem(item.ProductId, item.Quantity, DateTime.Now));
            }

            return new Cart(dto.UserId, DateTime.Now, cartItems);

        }
    }
}
using AplicationLayer;
using EnterpriseLayer;
using Mappers.Dtos.Requests;

namespace Mappers
{
    public class CartMapper : IMapper<CartRequestDTO, Cart>
    {
        public Cart ToEntity(CartRequestDTO dto) => new Cart
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            UserId = dto.UserId,
            Quantity = dto.Quantity
        };
    }
}
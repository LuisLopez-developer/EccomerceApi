using AplicationLayer;
using AplicationLayer.Exceptions;
using EnterpriseLayer;
using Mappers.Dtos.Requests;

namespace Mappers
{
    public class CartItemMapper : IMapper<ChangeItemQuantityDTO, CartItem>
    {
        public CartItem ToEntity(ChangeItemQuantityDTO dto)
        {
            var quantity = dto.Quantity;

            if (dto.Action == ChangeAction.increase_quantity)
            {
                quantity++;
            }
            else if (dto.Action == ChangeAction.decrease_quantity)
            {
                quantity--;
            }
            else
            {
                throw new ValidationException("Accion invalida");
            }

            return new CartItem(dto.ItemId, quantity);
        }
    }

}

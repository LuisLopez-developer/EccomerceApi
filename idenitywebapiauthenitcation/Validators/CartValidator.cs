using FluentValidation;
using Mappers.Dtos.Requests;

namespace EccomerceApi.Validators
{
    public class CartValidator : AbstractValidator<CartRequestDTO>
    {
        public CartValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("El id del producto es requerido");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("La cantidad es requerida");
        }
    }
}

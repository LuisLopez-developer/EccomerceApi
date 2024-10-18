using FluentValidation;
using Mappers.Dtos.Requests;

namespace EccomerceApi.Validators
{
    public class CartValidator : AbstractValidator<CartRequestDTO>
    {
        public CartValidator()
        {
            RuleFor(x => x.CartItems).NotEmpty().WithMessage("Se requiere Items.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Se requiere el id de un Usuario.");
        }
    }
}

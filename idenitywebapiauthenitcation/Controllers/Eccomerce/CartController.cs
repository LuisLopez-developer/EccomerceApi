using AplicationLayer.Sale;
using FluentValidation;
using Mappers.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers.Eccomerce
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AddCartUseCase<CartRequestDTO> _addCartUseCase;
        private readonly IValidator<CartRequestDTO> _validator;
        private readonly GetCartUseCase _cartUseCase;

        public CartController(
            AddCartUseCase<CartRequestDTO> addCartUseCase,
            IValidator<CartRequestDTO> validator,
            GetCartUseCase cartUseCase)
        {
            _addCartUseCase = addCartUseCase;
            _validator = validator;
            _cartUseCase = cartUseCase;
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddCart(CartRequestDTO cartRequest)
        {
            var result = await _validator.ValidateAsync(cartRequest);
            if (!result.IsValid)
            {
                return BadRequest(result.ToDictionary());
            }

            await _addCartUseCase.ExecuteAsync(cartRequest);

            // Retornar un 201
            return CreatedAtAction(nameof(AddCart), new { id = cartRequest.UserId }, cartRequest);
        }

        [HttpGet("cart")]
        public async Task<IActionResult> GetCarts()
        {
            var result = await _cartUseCase.ExecuteAsync();
            return Ok(result);
        }
    }
}

using AplicationLayer.Sale;
using EnterpriseLayer;
using FluentValidation;
using Mappers.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Models;
using Presenters.SaleViewModel;

namespace EccomerceApi.Controllers.Eccomerce
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AddCartUseCase<CartRequestDTO> _addCartUseCase;
        private readonly IValidator<CartRequestDTO> _validator;
        private readonly GetCartUseCase<Cart, CartDetailViewModel> _cartUseCase;
        private readonly GetCartSearchUseCase<CartModel> _cartSearchUseCase;
        private readonly UpdateCartUseCase<CartRequestDTO> _updateCartUseCase;
        private readonly DeleteCartUseCase _deleteCartUseCase;
        private readonly GetTotalProductQuantityByUserIdUseCase _getTotalProductQuantityByUserIdUseCase;

        public CartController(
            AddCartUseCase<CartRequestDTO> addCartUseCase,
            IValidator<CartRequestDTO> validator,
            GetCartUseCase<Cart, CartDetailViewModel> cartUseCase,
            GetCartSearchUseCase<CartModel> cartSearchUseCase,
            UpdateCartUseCase<CartRequestDTO> updateCartUseCase,
            DeleteCartUseCase deleteCartUseCase,
            GetTotalProductQuantityByUserIdUseCase getTotalProductQuantityByUserIdUseCase)
        {
            _addCartUseCase = addCartUseCase;
            _validator = validator;
            _cartUseCase = cartUseCase;
            _cartSearchUseCase = cartSearchUseCase;
            _updateCartUseCase = updateCartUseCase;
            _deleteCartUseCase = deleteCartUseCase;
            _getTotalProductQuantityByUserIdUseCase = getTotalProductQuantityByUserIdUseCase;
        }

        [HttpPost]
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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetCarts()
        {
            var result = await _cartUseCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartSearch(string userId)
        {
            var result = await _cartSearchUseCase.ExecuteAsync(c => c.UserId == userId);

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart(int cartId, CartRequestDTO cartRequest)
        {
            try
            {
                await _updateCartUseCase.ExecuteAsync(cartId, cartRequest);
                return NoContent(); 
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            try
            {
                await _deleteCartUseCase.ExecuteAsync(cartId);
                return NoContent(); // 204 si se eliminó correctamente
            }
            catch (Exception ex)
            {
                // Si no se encontró el carrito o hay otro error
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("total/{userId}")]
        public async Task<IActionResult> GetTotalProductQuantityByUserId(string userId)
        {
            var result = await _getTotalProductQuantityByUserIdUseCase.ExecuteAsync(userId);
            return Ok(result);
        }

    }
}

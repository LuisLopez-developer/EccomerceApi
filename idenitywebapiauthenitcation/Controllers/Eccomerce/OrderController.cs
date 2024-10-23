using AplicationLayer.Sale;
using Mappers.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers.Eccomerce
{
    [ApiController]
    [Route("api/eccomerce/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly GenerateOrderThroughCartUseCase _generateOrderThroughCart;

        public OrderController(GenerateOrderThroughCartUseCase generateOrderThroughCart)
        {
            _generateOrderThroughCart = generateOrderThroughCart;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateOrder(GenerateOrderPerCustomerDTO generateOrderPerCustomer)
        {
            await _generateOrderThroughCart.ExecuteAsync(generateOrderPerCustomer.CartId, generateOrderPerCustomer.PaymentMethodId);

            return CreatedAtAction(nameof(GenerateOrder), generateOrderPerCustomer);
        }

    }
}

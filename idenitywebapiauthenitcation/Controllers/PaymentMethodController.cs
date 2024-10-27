using AplicationLayer.Sale;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly GetAllPaymentMethodsUseCase _getAllPaymentMethodsUseCase;

        public PaymentMethodController(GetAllPaymentMethodsUseCase getAllPaymentMethodsUseCase)
        {
            _getAllPaymentMethodsUseCase = getAllPaymentMethodsUseCase;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var paymentMethods = await _getAllPaymentMethodsUseCase.ExecuteAsync();
                return Ok(paymentMethods);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

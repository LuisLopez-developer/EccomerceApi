using AplicationLayer.Sale;
using EnterpriseLayer;
using Mappers.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Presenters.SaleViewModel;

namespace EccomerceApi.Controllers.SystemControl
{
    [ApiController]
    [Route("api/system/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderForCustomerUseCase<OrderRequestDTO> _orderForCustomerUseCase;
        private readonly GetOrderDetailByIdUseCase<OrderViewModel> _orderDetailByIdUseCase;
        private readonly GetAllOrdersUseCase<Order, OrdersViewModel> _getAllOrdersUseCase;
        public OrderController(
            CreateOrderForCustomerUseCase<OrderRequestDTO> orderForCustomerUseCase, 
            GetOrderDetailByIdUseCase<OrderViewModel> getOrderDetailByIdUseCase,
            GetAllOrdersUseCase<Order, OrdersViewModel> getAllOrdersUseCase)
        {
            _orderForCustomerUseCase = orderForCustomerUseCase;
            _orderDetailByIdUseCase = getOrderDetailByIdUseCase;
            _getAllOrdersUseCase = getAllOrdersUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestDTO orderRequest)
        {
            await _orderForCustomerUseCase.ExecuteAsync(orderRequest);

            return CreatedAtAction(nameof(CreateOrder), new { id = orderRequest.CustomerDNI }, orderRequest);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var result = await _orderDetailByIdUseCase.ExecuteAsync(orderId);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _getAllOrdersUseCase.ExecuteAsync();
            return Ok(result);
        }

    }
}

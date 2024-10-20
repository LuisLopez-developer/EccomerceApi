using AplicationLayer.Sale;
using EnterpriseLayer;
using Mappers.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Models;
using Presenters.SaleViewModel;
using Repository;

namespace EccomerceApi.Controllers.SystemControl
{
    [ApiController]
    [Route("api/system/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderForCustomerUseCase<OrderRequestDTO> _orderForCustomerUseCase;
        private readonly GetOrderDetailByIdUseCase<OrderViewModel> _orderDetailByIdUseCase;
        private readonly GetAllOrdersUseCase<Order, OrdersViewModel> _getAllOrdersUseCase;
        private readonly GetEntitiesSearchUseCase<OrderModel, Order, OrdersViewModel> _getEntitiesSearchUseCase;

        public OrderController(
            CreateOrderForCustomerUseCase<OrderRequestDTO> orderForCustomerUseCase, 
            GetOrderDetailByIdUseCase<OrderViewModel> getOrderDetailByIdUseCase,
            GetAllOrdersUseCase<Order, OrdersViewModel> getAllOrdersUseCase,
            GetEntitiesSearchUseCase<OrderModel, Order, OrdersViewModel> getEntitiesSearchUseCase)
        {
            _orderForCustomerUseCase = orderForCustomerUseCase;
            _orderDetailByIdUseCase = getOrderDetailByIdUseCase;
            _getAllOrdersUseCase = getAllOrdersUseCase;
            _getEntitiesSearchUseCase = getEntitiesSearchUseCase;
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

        [HttpGet("by-filter")]
        public async Task<IActionResult> GetOrdersByCustomer([FromQuery] OrderFilterDTO filter)
        {
            var orders = await _getEntitiesSearchUseCase.ExecuteAsync(o =>
                (string.IsNullOrEmpty(filter.CustomerDNI) || o.CustomerDNI.Contains(filter.CustomerDNI)) &&
                (!filter.StatusId.HasValue || o.StatusId == filter.StatusId) &&
                (!filter.PaymentMethodId.HasValue || o.PaymentMethodId == filter.PaymentMethodId) &&
                (!filter.CreatedFrom.HasValue || o.CreatedAt >= filter.CreatedFrom.Value) &&
                (!filter.CreatedTo.HasValue || o.CreatedAt <= filter.CreatedTo.Value) &&
                (string.IsNullOrEmpty(filter.WorkerId) || o.WorkerId == filter.WorkerId)
            );
            return Ok(orders);
        }

    }
}

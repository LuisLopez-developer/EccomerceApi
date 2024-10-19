using EnterpriseLayer;
using System.ComponentModel.DataAnnotations;

namespace AplicationLayer.Sale
{
    public class CreateOrderForCustomerUseCase<TDTO>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper<TDTO, Order> _mapper;

        public CreateOrderForCustomerUseCase(IRepository<Order> orderRepository, IMapper<TDTO, Order> mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO orderDTO)
        {
     
            var order = _mapper.ToEntity(orderDTO);

            if (order.OrderDetails.Count <= 0)
                throw new ValidationException("La orden debe contener detalles.");

            if (order.Total <= 0)
                throw new ValidationException("La orden debe tener un total mayor a S/ 0.00.");

            await _orderRepository.AddAsync(order);
        }

    }
}

using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class CreateOrderForCustomerUseCase<TDTO>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper<TDTO, Order> _mapper;
        private readonly IRepository<Product> _productRepository;

        public CreateOrderForCustomerUseCase(IRepository<Order> orderRepository, IMapper<TDTO, Order> mapper, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(TDTO orderDTO)
        {
            var order = _mapper.ToEntity(orderDTO);

            if (order.OrderDetails.Count <= 0)
                throw new ValidationException("La orden debe contener detalles.");

            if (order.Total <= 0)
                throw new ValidationException("La orden debe tener un total mayor a S/ 0.00.");

            foreach (var detail in order.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);

                if (product == null)
                    throw new ValidationException($"El producto con ID {detail.ProductId} no existe.");

                if (product.Existence < detail.Quantity)
                    throw new ValidationException($"No hay suficiente cantidad del producto con ID {detail.ProductId}.");

                // Validar si el precio del actual producto, es mayor al 60% del nuevo precio
                if (product.Cost * 0.6m > detail.UnitPrice)
                    throw new ValidationException($"El precio del producto con ID {detail.ProductId} no puede ser menor al 60% del costo.");

                product.Existence -= detail.Quantity;
                await _productRepository.UpdateAsync(product.Id, product);
            }
            order.StatusId = 4; // Estado: Entregado
            await _orderRepository.AddAsync(order);
        }

    }
}
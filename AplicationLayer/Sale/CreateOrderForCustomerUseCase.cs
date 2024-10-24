using AplicationLayer.Exceptions;
using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class CreateOrderForCustomerUseCase<TDTO>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper<TDTO, Order> _mapper;
        private readonly IRepository<Product> _productRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IReniecService _reniecService;

        public CreateOrderForCustomerUseCase(
            IRepository<Order> orderRepository, 
            IMapper<TDTO, Order> mapper, 
            IRepository<Product> productRepository,
            IPeopleRepository peopleRepository,
            IReniecService reniecService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _peopleRepository = peopleRepository;
            _reniecService = reniecService;
        }

        public async Task ExecuteAsync(TDTO orderDTO)
        {
            var order = _mapper.ToEntity(orderDTO);

            int customerId;

            if (!await _peopleRepository.ExistByDNIAsync(order.CustomerDNI))
            {
                var people = await _reniecService.GetPersonDataByDNIAsync(order.CustomerDNI) ?? throw new ValidationException("El DNI ingresado no existe.");
                customerId = await _peopleRepository.AddAsync(people);
            }
            else
            {
                customerId = await _peopleRepository.GetIdByDNIAsync(order.CustomerDNI);
            }

            // Crear una nueva instancia de Order con el ID del cliente
            order = new Order.Builder()
                .SetId(order.Id)
                .SetCustomerId(customerId)
                .SetCustomerDNI(order.CustomerDNI)
                .SetCreatedByUserId(order.CreatedByUserId)
                .SetCreatedByUserName(order.CreatedByUserName)
                .SetStatusId(order.StatusId)
                .SetPaymentMethodId(order.PaymentMethodId)
                .SetPaymentMethodName(order.PaymentMethodName)
                .SetTotal(order.Total)
                .SetCreatedAt(order.CreatedAt)
                .SetOrderDetails(order.OrderDetails)
                .Build();

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

                // La ganancia no debe ser menor al 10% costo
                if (detail.UnitPrice < product.Cost * 1.1m)
                    throw new ValidationException($"La ganancia del producto con ID {detail.ProductId} no debe ser menor al 10% del costo.");

                product.Existence -= detail.Quantity;
                await _productRepository.UpdateAsync(product.Id, product);
            }

            await _orderRepository.AddAsync(order);
        }

    }
}
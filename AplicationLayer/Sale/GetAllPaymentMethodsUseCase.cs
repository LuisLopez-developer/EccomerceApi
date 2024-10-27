using EnterpriseLayer;

namespace AplicationLayer.Sale
{
    public class GetAllPaymentMethodsUseCase
    {
        private readonly IGetRepository<PaymentMethod> _paymentMethodRepository;

        public GetAllPaymentMethodsUseCase(IGetRepository<PaymentMethod> paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<IEnumerable<PaymentMethod>> ExecuteAsync()
        {
            return await _paymentMethodRepository.GetAllAsync();
        }
    }
}

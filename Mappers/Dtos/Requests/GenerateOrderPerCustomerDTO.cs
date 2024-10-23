namespace Mappers.Dtos.Requests
{
    public class GenerateOrderPerCustomerDTO
    {
        public required int CartId { get; set; }
        public required int PaymentMethodId { get; set; }
    }
}

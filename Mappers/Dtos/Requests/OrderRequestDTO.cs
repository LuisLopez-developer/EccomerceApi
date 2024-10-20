namespace Mappers.Dtos.Requests
{
    public class OrderRequestDTO
    {
        public string CustomerDNI { get; set; }
        public string CustomerEmail { get; set; }
        public string WorkerId { get; set; }
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailRequestDTO> OrderItems { get; set; }
    }

    public class OrderDetailRequestDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}

namespace Mappers.Dtos.Response
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CreatedByUserId { get; set; }
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}

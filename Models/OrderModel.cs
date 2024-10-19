namespace Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string CustomerDNI { get; set; }
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Total { get; set; }
        public string WorkerId { get; set; } // Sirve para saber quien creo la orden
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
    }
}

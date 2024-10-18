namespace Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        public decimal Total { get; set; }
        public string CreatedByUserId { get; set; } // Sirve para saber quien creo la orden
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
    }
}

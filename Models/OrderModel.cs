namespace Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public virtual OrderStatusModel Status { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethodModel PaymentMethod { get; set; }
        public decimal Total { get; set; }
        // Relación con UserModel para Worker
        public string? WorkerId { get; set; }
        public virtual UserModel? Worker { get; set; }

        // Relación con PeopleModel para Customer
        public int CustomerId { get; set; }
        public virtual PeopleModel Customer { get; set; }

        public DateTime CreatedAt { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
    }
}

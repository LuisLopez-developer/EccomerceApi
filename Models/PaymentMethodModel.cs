namespace Models
{
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrderModel> Orders { get; set; } = [];
    }
}

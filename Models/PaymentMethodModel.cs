namespace Models
{
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<OrderModel> Orders { get; set; } = new List<OrderModel>();
    }
}

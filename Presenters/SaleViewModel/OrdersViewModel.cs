namespace Presenters.SaleViewModel
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CreatedByUserName { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCreatedBySameUser { get; set; }
    }
}

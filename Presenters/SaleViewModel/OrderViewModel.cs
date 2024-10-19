namespace Presenters.SaleViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StatusID { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Total { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }

    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductViewModel ProductDetails { get; set; }
    }

    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}

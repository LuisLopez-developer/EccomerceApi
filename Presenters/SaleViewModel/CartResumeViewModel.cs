namespace Presenters.SaleViewModel
{
    public class CartResumeViewModel
    {
        public int Id { get; set; }
        public decimal Total => CartItems.Sum(cartItem => cartItem.Total);
        public List<CartItemResumenViewModel> CartItems { get; set; }
    }

    public class CartItemResumenViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int ExistentQuantity { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;
    }
}

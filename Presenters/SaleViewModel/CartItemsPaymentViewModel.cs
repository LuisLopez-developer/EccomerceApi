namespace Presenters.SaleViewModel
{
    public class CartItemsPaymentViewModel
    {
        public int Id { get; set; }
        public List<CartItemPaymentViewModel> CartItems { get; set; }
 
    }
    public class CartItemPaymentViewModel
    {
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}

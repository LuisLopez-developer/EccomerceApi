namespace Presenters.SaleViewModel
{
    public class CartDetailViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

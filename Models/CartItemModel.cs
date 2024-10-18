namespace Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

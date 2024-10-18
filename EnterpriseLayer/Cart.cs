namespace EnterpriseLayer
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItem> CartItems { get; }

        // Para el mapper
        public Cart(string userId, DateTime createdAt, List<CartItem> cartItems)
        {
            UserId = userId;
            CreatedAt = createdAt;
            CartItems = cartItems;
        }

        public Cart(int id, string userId, DateTime createdAt, List<CartItem> cartItems)
        {
            Id = id;
            UserId = userId;
            CreatedAt = createdAt;
            CartItems = cartItems;
        }

    }
}

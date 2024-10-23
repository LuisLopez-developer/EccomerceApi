namespace EnterpriseLayer
{
    public class CartItem
    {
        public int Id { get; }
        public int ProductId { get; }
        public int Quantity { get; }
        public DateTime CreatedAt { get; }

        public CartItem(int productId, int quantity, DateTime createdAt)
        {
            ProductId = productId;
            Quantity = quantity;
            CreatedAt = createdAt;
        }

        public CartItem(int id, int productId, int quantity, DateTime createdAt)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            CreatedAt = createdAt;
        }

        public CartItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;        
        }

    }
}

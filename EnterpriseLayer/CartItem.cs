namespace EnterpriseLayer
{
    public class CartItem
    {
        public int Id { get; }
        public int ProductId { get; }
        public int Quantity { get; }
        public Product Product { get; set; }
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

        // Para el cambio de cantidad de un item en el carrito
        public CartItem(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;        
        }

    }
}

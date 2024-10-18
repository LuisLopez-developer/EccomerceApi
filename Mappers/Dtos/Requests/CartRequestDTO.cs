namespace Mappers.Dtos.Requests
{
    public class CartRequestDTO
    {
        public string UserId { get; set; }
        public List<CartItemRequestDTO> CartItems { get; set; }
    }

    public class CartItemRequestDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

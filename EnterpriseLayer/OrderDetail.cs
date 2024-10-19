namespace EnterpriseLayer
{
    public class OrderDetail
    {
        public int ProductId { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public decimal TotalPrice { get; }

        public OrderDetail(int productID, int quantity, decimal unitPrice, decimal totalPrice)
        {
            ProductId = productID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
        }

        public OrderDetail(int productID, int quantity, decimal unitPrice)
        {
            ProductId = productID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = getTotalPrice();
        }

        private decimal getTotalPrice()
            => Quantity * UnitPrice;
    }
}

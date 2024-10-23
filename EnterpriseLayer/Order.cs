namespace EnterpriseLayer
{
    public class Order
    {
        public int Id { get; private set; }
        public string CustomerDNI { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
        public string CustomerEmail { get; private set; } = string.Empty;
        public string CreatedByUserId { get; private set; } = string.Empty;
        public string CreatedByUserName { get; private set; } = string.Empty;
        public int StatusId { get; private set; }
        public string StatusName { get; private set; } = string.Empty;
        public int PaymentMethodId { get; private set; }
        public string PaymentMethodName { get; private set; } = string.Empty;
        public decimal Total { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();

        // Constructor privado, solo accesible por el Builder
        private Order() { }

        public bool IsCreatedBySameUserName() => UserName?.Equals(CreatedByUserName) ?? false;

        // Implementación del patrón Builder para la clase Order
        public class Builder
        {
            private Order _order;

            public Builder()
            {
                _order = new Order();
            }

            public Builder SetId(int id)
            {
                _order.Id = id;
                return this;
            }

            public Builder SetCustomerDNI(string customerDNI)
            {
                _order.CustomerDNI = customerDNI;
                return this;
            }

            public Builder SetUserName(string userName)
            {
                _order.UserName = userName;
                return this;
            }

            public Builder SetCustomerEmail(string email)
            {
                _order.CustomerEmail = email;
                return this;
            }

            public Builder SetCreatedByUserId(string createdByUserId)
            {
                _order.CreatedByUserId = createdByUserId;
                return this;
            }

            public Builder SetCreatedByUserName(string createdByUserName)
            {
                _order.CreatedByUserName = createdByUserName;
                return this;
            }

            public Builder SetStatusId(int statusId)
            {
                _order.StatusId = statusId;
                return this;
            }

            public Builder SetStatusName(string statusName)
            {
                _order.StatusName = statusName;
                return this;
            }

            public Builder SetPaymentMethodId(int paymentMethodId)
            {
                _order.PaymentMethodId = paymentMethodId;
                return this;
            }

            public Builder SetPaymentMethodName(string paymentMethodName)
            {
                _order.PaymentMethodName = paymentMethodName;
                return this;
            }

            public Builder SetTotal(decimal total)
            {
                _order.Total = total;
                return this;
            }

            public Builder SetCreatedAt(DateTime createdAt)
            {
                _order.CreatedAt = createdAt;
                return this;
            }

            public Builder SetOrderDetails(List<OrderDetail> orderDetails)
            {
                _order.OrderDetails = orderDetails;
                _order.Total = orderDetails.Sum(od => od.TotalPrice); // Calcula el total automáticamente
                return this;
            }

            public Order Build()
            {
                // Aquí podes añadir validaciones si es necesario
                /*
                if (_order.OrderDetails == null || !_order.OrderDetails.Any())
                {
                    throw new InvalidOperationException("El pedido debe tener al menos un OrderDetail");
                }
                */

                return _order;
            }
        }
    }
}

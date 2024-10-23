using AplicationLayer;
using EnterpriseLayer;
using Mappers.Dtos.Requests;

namespace Mappers
{
    public class OrderMapper : IMapper<OrderRequestDTO, Order>
    {
        public Order ToEntity(OrderRequestDTO dto)
        {
            var orderDetail = new List<OrderDetail>();
            foreach (var item in dto.OrderItems)
            {
                orderDetail.Add(new OrderDetail(item.ProductId, item.Quantity, item.UnitPrice));
            }
            // Order(dto.CustomerDNI, dto.CustomerEmail, dto.WorkerId, dto.StatusId, dto.PaymentMethodId, dto.CreatedAt, orderDetail)
            return new Order.Builder()
                        .SetCustomerDNI(dto.CustomerDNI)
                        .SetCustomerEmail(dto.CustomerEmail)
                        .SetCreatedByUserId(dto.WorkerId)
                        .SetStatusId(dto.StatusId)
                        .SetPaymentMethodId(dto.PaymentMethodId)
                        .SetCreatedAt(dto.CreatedAt)
                        .SetOrderDetails(orderDetail)
                        .Build();
        }
    }   
}

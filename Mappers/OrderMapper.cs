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
            return new Order(dto.CustomerDNI, dto.WorkerId, dto.StatusId, dto.PaymentMethodId, dto.CreatedAt, orderDetail);
        }
    }
}

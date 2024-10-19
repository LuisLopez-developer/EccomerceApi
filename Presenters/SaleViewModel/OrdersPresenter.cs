using AplicationLayer;
using EnterpriseLayer;

namespace Presenters.SaleViewModel
{
    public class OrdersPresenter : IPresenter<Order, OrdersViewModel>
    {
        public IEnumerable<OrdersViewModel> Present(IEnumerable<Order> orders)
        {
            var ordersViewModels = orders.Select(order => new OrdersViewModel()
            {
                CreatedAt = order.CreatedAt,
                CreatedByUserName = order.CreatedByUserName,
                Id = order.Id,
                PaymentMethod = order.PaymentMethodName,
                Status = order.StatusName,
                Total = order.Total,
                UserName = order.UserName,
                IsCreatedBySameUser = order.IsCreatedBySameUserName()
            }).ToList();

            return ordersViewModels;
        }
    }
}

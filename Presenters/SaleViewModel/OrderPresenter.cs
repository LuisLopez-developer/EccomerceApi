using AplicationLayer;
using EnterpriseLayer;

namespace Presenters.SaleViewModel
{
    public class OrderPresenter : IOrderDetailPresenter<OrderViewModel>
    {
        public OrderViewModel Present(Order order, IEnumerable<Product> products)
        {
            var orderDetailViewModels = new List<OrderDetailViewModel>();

            foreach (var orderDetail in order.OrderDetails)
            {
                var product = products.FirstOrDefault(p => p.Id == orderDetail.ProductId);
                if (product != null)
                {
                    var orderDetailViewModel = new OrderDetailViewModel
                    {
                        ProductId = product.Id,
                        Quantity = orderDetail.Quantity,
                        UnitPrice = orderDetail.UnitPrice,
                        TotalPrice = orderDetail.TotalPrice,
                        ProductDetails = new ProductViewModel
                        {
                            Name = product.Name,
                            Description = product.Description,
                            CategoryId = product.ProductCategoryId,
                        }
                    };
                    orderDetailViewModels.Add(orderDetailViewModel);
                }
            }

            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                UserId = order.UserId,
                StatusID = order.StatusId,
                PaymentMethodId = order.PaymentMethodId,
                Total = order.Total,
                CreatedByUserId = order.CreatedByUserId,
                CreatedAt = order.CreatedAt,
                OrderDetails = orderDetailViewModels
            };

            return orderViewModel;
        }
    }
}

using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order entity)
        {
            var orderModel = new OrderModel
            {
                UserId = entity.UserId,
                CreatedByUserId = entity.CreatedByUserId,
                StatusId = entity.StatusId,
                PaymentMethodId = entity.PaymentMethodId,
                Total = entity.Total,
                CreatedAt = entity.CreatedAt
            };

            // Obtener detalles de productos
            var productIds = entity.OrderDetails.Select(od => od.ProductId).ToList();
            var products = await _dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            // Añadir detalles de la orden
            orderModel.OrderDetails = entity.OrderDetails.Select(od =>
            {
                var product = products.FirstOrDefault(p => p.Id == od.ProductId);
                return new OrderDetailModel
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitCost = product?.Cost ?? 0,
                    UnitPrice = od.UnitPrice,
                    TotalCost = (int)(od.Quantity * (product?.Cost ?? 0)),
                    TotalPrice = od.TotalPrice
                };
            }).ToList();

            await _dbContext.Orders.AddAsync(orderModel);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
            => await (from order in _dbContext.Orders
                      join paymentMethod in _dbContext.PaymentMethods
                          on order.PaymentMethodId equals paymentMethod.Id
                      join orderStatus in _dbContext.OrderStatuses
                          on order.StatusId equals orderStatus.Id
                      join user in _dbContext.Users
                          on order.UserId equals user.Id
                      join createdByUser in _dbContext.Users
                          on order.CreatedByUserId equals createdByUser.Id
                      select new Order(
                          order.Id,
                          user.UserName ?? "",
                          createdByUser.UserName ?? "",
                          orderStatus.Name,
                          paymentMethod.Name,
                          order.Total,
                          order.CreatedAt
                          )
                      ).ToListAsync();

        public async Task<Order> GetByIdAsync(int id)
        {
            var orderModel = await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .FirstAsync(o => o.Id == id);

            return new Order(
                orderModel.Id,
                orderModel.UserId,
                orderModel.CreatedByUserId,
                orderModel.StatusId,
                orderModel.PaymentMethodId,
                orderModel.CreatedAt,
                orderModel.OrderDetails
                .Where(od => od.OrderId == orderModel.Id)
                .Select(od => new OrderDetail(
                    od.ProductId,
                    od.Quantity,
                    od.UnitPrice,
                    od.TotalPrice
                )).ToList()
            );
        }

        public Task UpdateAsync(int id, Order entity)
        {
            throw new NotImplementedException();
        }
    }
}

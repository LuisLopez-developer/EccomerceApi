using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class OrderRepository : IRepository<Order>, IRepositorySearch<OrderModel, Order>
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
                StatusId = entity.StatusId,
                PaymentMethodId = entity.PaymentMethodId,
                Total = entity.Total,
                WorkerId = entity.CreatedByUserId,
                CustomerId = entity.CustomerId,
                CreatedAt = entity.CreatedAt == default ? DateTime.Now : entity.CreatedAt
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
        {
            return await _dbContext.Orders
                    .Include(o => o.Status)
                    .Include(o => o.PaymentMethod)
                    .Include(o => o.Worker)
                    .ThenInclude(w => w.People)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Users) // Asegúrate de que esta relación es correcta
                    .Select(order => new Order.Builder()
                        .SetId(order.Id)
                        .SetCustomerDNI(order.Customer.DNI ?? "")
                        .SetUserName(order.Worker.UserName ?? "")
                        .SetStatusName(order.Status.Name)
                        .SetPaymentMethodName(order.PaymentMethod.Name)
                        .SetTotal(order.Total)
                        .SetCreatedAt(order.CreatedAt)
                        .Build())
                    .ToListAsync();
        }


        public async Task<IEnumerable<Order>> GetAsync(Expression<Func<OrderModel, bool>> predicate)
        {
            return await _dbContext.Orders
                    .Include(o => o.Status)
                    .Include(o => o.PaymentMethod)
                    .Include(o => o.Worker)
                    .ThenInclude(w => w.People)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Users) // Asegúrate de que esta relación es correcta
                    .Select(order => new Order.Builder()
                        .SetId(order.Id)
                        .SetCustomerDNI(order.Customer.DNI ?? "")
                        .SetUserName(order.Worker.UserName ?? "")
                        .SetStatusName(order.Status.Name)
                        .SetPaymentMethodName(order.PaymentMethod.Name)
                        .SetTotal(order.Total)
                        .SetCreatedAt(order.CreatedAt)
                        .Build())
                    .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {


            return null;
        }

        public Task UpdateAsync(int id, Order entity)
        {
            throw new NotImplementedException();
        }
    }
}

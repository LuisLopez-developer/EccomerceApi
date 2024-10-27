using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PaymentMethodRepository : IGetRepository<PaymentMethod>
    {
        private readonly AppDbContext _context;
        public PaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<PaymentMethod>> IGetRepository<PaymentMethod>.GetAllAsync()
        {
            var paymentMethods = await _context.PaymentMethods.ToListAsync();

            return paymentMethods.Select(pm => new PaymentMethod
            {
                Id = pm.Id,
                Name = pm.Name,
                Description = pm.Description
            });
        }
    }
}

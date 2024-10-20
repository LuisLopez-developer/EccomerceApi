using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class StatusRepository : IRepository<Status>
    {
        private readonly AppDbContext _context;

        public StatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Status entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _context.OrderStatuses.Select(s => new Status(s.Id, s.Name)).ToListAsync();
        }

        public Task<Status> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Status entity)
        {
            throw new NotImplementedException();
        }
    }
}

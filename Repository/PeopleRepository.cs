using AplicationLayer;
using Data;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AppDbContext _dbContext;

        public PeopleRepository(AppDbContext appDbContext) 
        {
            _dbContext = appDbContext;
        }

        public async Task<int> AddAsync(People entity)
        {
            var peopleModel = new PeopleModel
            {
                DNI = entity.DNI,
                Name = entity.Name,
                LastName = entity.LastName,
                Address = entity.Address
            };

            await _dbContext.Peoples.AddAsync(peopleModel);
            await _dbContext.SaveChangesAsync();

            return peopleModel.Id;
        }

        public async Task<bool> ExistByDNIAsync(string dni)
        {
            return await _dbContext.Peoples.AnyAsync(p => p.DNI == dni);
        }

        public Task<int> GetIdByDNIAsync(string dni)
        {
            return _dbContext.Peoples
                .Where(p => p.DNI == dni)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
        }
    }
}

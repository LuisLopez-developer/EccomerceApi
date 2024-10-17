using Data;
using Data.Entity;
using EccomerceApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services
{
    public class StateService : IState
    {
        private readonly AppDbContext _identityDbContext;

        public StateService(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<List<State>> GetAllAsync()
        {
            var statesList = await _identityDbContext.States.ToListAsync();
            return statesList;
        }
    }
}

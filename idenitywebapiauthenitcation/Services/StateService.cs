using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Services
{
    public class StateService : IState
    {
        private readonly IdentityDbContext _identityDbContext;

        public StateService(IdentityDbContext identityDbContext)
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

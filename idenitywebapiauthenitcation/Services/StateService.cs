using Data;
using EccomerceApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EccomerceApi.Services
{
    public class StateService : IState
    {
        private readonly AppDbContext _identityDbContext;

        public StateService(AppDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<List<StateModel>> GetAllAsync()
        {
            var statesList = await _identityDbContext.States.ToListAsync();
            return statesList;
        }
    }
}

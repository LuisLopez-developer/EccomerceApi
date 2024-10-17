using EccomerceApi.Interfaces;
using EccomerceApi.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {

        private readonly IState _state;

        public StateController(IState state)
        {
            _state = state;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _state.GetAllAsync();
            var viewModel = response.Select(s => new StateViewModel
            {
                Id = s.Id,
                Name = s.Name
            });

            return Ok(viewModel);
        }

    }
}

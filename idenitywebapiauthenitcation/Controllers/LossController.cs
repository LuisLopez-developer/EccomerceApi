using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LossController : ControllerBase
    {

        private readonly ILoss _loss;

        public LossController(ILoss loss)
        {
            _loss = loss;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _loss.GetAllAsync();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName(string? name)
        {
            IEnumerable<LossesViewModel> response;

            if (string.IsNullOrWhiteSpace(name))
            {
                response = await _loss.GetAllAsync(); // Si name es nulo o vacío, obtenemos todas las perdidas
            }
            else
            {
                response = await _loss.SearchAsync(name);
            }

            return Ok(response);
        }
    }
}

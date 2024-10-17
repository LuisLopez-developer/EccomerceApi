using EccomerceApi.Interfaces;
using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _loss.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(LossCreateModel lossCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLossReason = await _loss.CreateAsync(lossCreateModel);
            return CreatedAtAction(nameof(GetById), new { id = createdLossReason.Id }, createdLossReason);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LossCreateModel lossCreateModel)
        {
            var existingPorduct = await _loss.UpdateAsync(id, lossCreateModel);
            if (!existingPorduct)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}

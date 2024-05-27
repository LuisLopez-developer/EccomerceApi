using EccomerceApi.Interfaces;
using EccomerceApi.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LossReasonController : ControllerBase
    {
        private readonly ILossReason _lossReason;

        public LossReasonController(ILossReason lossReason)
        {
            _lossReason = lossReason;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _lossReason.GetAllAsync();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _lossReason.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(LossReasonViewModel lossReasonViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLossReason = await _lossReason.CreateAsync(lossReasonViewModel);
            return CreatedAtAction(nameof(GetById), new { id = createdLossReason.Id }, createdLossReason);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LossReasonViewModel lossReasonViewModel)
        {
            var existingLossReason = await _lossReason.UpdateAsync(id, lossReasonViewModel);
            if (!existingLossReason)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _lossReason.DeleteAsync(id);
            if (!deleted)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}

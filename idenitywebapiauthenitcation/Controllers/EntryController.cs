using EccomerceApi.Interfaces;
using EccomerceApi.Model.CreateModel;
using EccomerceApi.Model.ViewModel;
using EccomerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IEntry _entry;

        public EntryController(IEntry entry)
        {
            _entry = entry;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _entry.GetAllAsync();
            return Ok(response);
        }

       

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EntryViewModel>>> FilterByDateAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate == default || endDate == default)
            {
                return BadRequest("startDate and endDate are required.");
            }

            var entries = await _entry.FilterByDateAsync(startDate, endDate);
            return Ok(entries);
        }
    }
}

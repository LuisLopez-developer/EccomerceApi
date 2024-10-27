using AplicationLayer.UserPeople;
using Mappers.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly AddPeopleUseCase<AddPeopleDTO> _peopleUseCase;

        public PeopleController(AddPeopleUseCase<AddPeopleDTO> peopleUseCase)
        {
            _peopleUseCase = peopleUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddPeopleDTO addPeopleDTO)
        {
            try
            {
                await _peopleUseCase.ExecuteAsync(addPeopleDTO, addPeopleDTO.UserId);
                return CreatedAtAction(nameof(Create), new { id = addPeopleDTO.DNI }, addPeopleDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

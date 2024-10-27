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
        private readonly IsUserLinkedToPersonUseCase _isUserLinkedToPersonUseCase;
        public PeopleController(AddPeopleUseCase<AddPeopleDTO> peopleUseCase, IsUserLinkedToPersonUseCase isUserLinkedToPersonUseCase)
        {
            _peopleUseCase = peopleUseCase;
            _isUserLinkedToPersonUseCase = isUserLinkedToPersonUseCase;
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> IsUserLinkedToPerson(string userId)
        {
            try
            {
                var isLinked = await _isUserLinkedToPersonUseCase.ExecuteAsync(userId);
                return Ok(isLinked);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

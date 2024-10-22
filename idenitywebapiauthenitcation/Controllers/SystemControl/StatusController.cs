using AplicationLayer.GenericUseCases;
using EnterpriseLayer;
using Microsoft.AspNetCore.Mvc;
using Presenters.SaleViewModel;

namespace EccomerceApi.Controllers.SystemControl
{
    [ApiController]
    [Route("api/system/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly GetAllEntitiesUseCase<Status, StatusViewModel> _getAllStatusesUseCase;

        public StatusController(GetAllEntitiesUseCase<Status, StatusViewModel> getAllStatusesUseCase)
        {
            _getAllStatusesUseCase = getAllStatusesUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var result = await _getAllStatusesUseCase.ExecuteAsync();
            return Ok(result);
        }
    }
}

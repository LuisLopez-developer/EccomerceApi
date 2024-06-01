using EccomerceApi.Interfaces.ProductInterfaces;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecificationController : ControllerBase
    {
        private readonly IProductSpecification _productSpecificationService;

        public ProductSpecificationController(IProductSpecification productSpecificationService)
        {
            _productSpecificationService = productSpecificationService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create/{productId}")]
        public async Task<IActionResult> Create(int productId, ProductSpecificationViewModel productSpecification)
        {
            try
            {
                var createdSpecification = await _productSpecificationService.CreateAsync(productId, productSpecification);
                return Ok(createdSpecification);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error interno del servidor.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productSpecificationService.DeleteAsync(id);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getbyproductid/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var specification = await _productSpecificationService.SearchByProductIdAsync(productId);
            if (specification == null)
            {
                return NotFound();
            }
            return Ok(specification);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, ProductSpecificationViewModel productSpecification)
        {
            var updated = await _productSpecificationService.UpdateAsync(id, productSpecification);
            if (updated)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

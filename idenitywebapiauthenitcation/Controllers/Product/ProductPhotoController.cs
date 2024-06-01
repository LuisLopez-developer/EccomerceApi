using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPhotoController : ControllerBase
    {
        private readonly IProductPhoto _productPhotoService;

        public ProductPhotoController(IProductPhoto productPhotoService)
        {
            _productPhotoService = productPhotoService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create/{productId}")]
        public async Task<IActionResult> Create(int productId, List<ProductPhotoViewModel> productPhotos)
        {
            try
            {
                var createdPhotos = await _productPhotoService.CreateAsync(productId, productPhotos);
                return Ok(createdPhotos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
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
            var deleted = await _productPhotoService.DeleteAsync(id);
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
            var photos = await _productPhotoService.SearchByProductIdAsync(productId);
            return Ok(photos);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, ProductPhotoViewModel productPhoto)
        {
            var updated = await _productPhotoService.UpdateAsync(id, productPhoto);
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

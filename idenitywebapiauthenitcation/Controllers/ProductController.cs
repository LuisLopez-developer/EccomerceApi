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
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _product.GetAllAsync();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName(string? name)
        {
            IEnumerable<ProductViewModel> response;

            if (string.IsNullOrWhiteSpace(name))
            {  
                response = await _product.GetAllAsync(); // Si name es nulo o vacío, obtenemos todas las categorías
            }
            else
            {
                response = await _product.SearchAsync(name);
            }

            return Ok(response);
        }
    }
}

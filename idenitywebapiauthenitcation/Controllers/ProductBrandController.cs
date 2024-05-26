using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandController : ControllerBase
    {
        private readonly IProductBrand _productBrand;

        public ProductBrandController(IProductBrand productBrand)
        {
            _productBrand = productBrand;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productBrand.GetAllAsync();
            var viewModel = response.Select(pb => new ProductBrandViewModel
            {
                Id = pb.Id,
                Name = pb.Name
            });

            return Ok(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _productBrand.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            var viewModel = new ProductBrandViewModel
            {
                Id = response.Id,
                Name = response.Name
            };

            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName(string? name)
        {
            IEnumerable<ProductBrand> response;

            if (string.IsNullOrWhiteSpace(name))
            {
                // Si name es nulo o vacío, obtenemos todas las categorías
                response = await _productBrand.GetAllAsync();
            }
            else
            {
                // Si name tiene un valor, realizamos la búsqueda
                response = await _productBrand.SearchAsync(name);
            }

            var viewModel = response.Select(pd => new ProductBrandViewModel
            {
                Id = pd.Id,
                Name = pd.Name
            });

            return Ok(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ProductBrand productBrand)
        {
            var response = await _productBrand.CreateAsync(productBrand);

            var viewModel = new ProductBrandViewModel
            {
                Id = response.Id,
                Name = response.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = viewModel.Id }, viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductBrand productBrand)
        {
            var existingPorductBrand = await _productBrand.UpdateAsync(id, productBrand);
            if (!existingPorductBrand)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _productBrand.DeleteAsync(id);
            if (!blog)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}

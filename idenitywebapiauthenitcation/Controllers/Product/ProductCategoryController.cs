using Data.Entity;
using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategory _prodCategoryService;

        public ProductCategoryController(IProductCategory productService)
        {
            _prodCategoryService = productService;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _prodCategoryService.GetAllAsync();
            var viewModel = response.Select(pc => new ProductCategoryViewModel
            {
                Id = pc.Id,
                Name = pc.Name
            });

            return Ok(viewModel);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _prodCategoryService.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            var viewModel = new ProductCategoryViewModel
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
            IEnumerable<ProductCategory> response;

            if (string.IsNullOrWhiteSpace(name))
            {
                // Si name es nulo o vacío, obtenemos todas las categorías
                response = await _prodCategoryService.GetAllAsync();
            }
            else
            {
                // Si name tiene un valor, realizamos la búsqueda
                response = await _prodCategoryService.SearchAsync(name);
            }

            var viewModel = response.Select(pc => new ProductCategoryViewModel
            {
                Id = pc.Id,
                Name = pc.Name
            });

            return Ok(viewModel);
        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ProductCategory productCategory)
        {
            var response = await _prodCategoryService.CreateAsync(productCategory);

            var viewModel = new ProductCategoryViewModel
            {
                Id = response.Id,
                Name = response.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = viewModel.Id }, viewModel);
        }


        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCategory productCategory)
        {
            var existingPorductCategory = await _prodCategoryService.UpdateAsync(id, productCategory);
            if (!existingPorductCategory)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _prodCategoryService.DeleteAsync(id);
            if (!blog)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // controladores enfocados para el eccomerce
        [HttpGet("GetAllPublic")]
        public async Task<IActionResult> GetAllPublic()
        {
            var response = await _prodCategoryService.GetAllAsync();
            var viewModel = response.Select(pc => new ProductCategoryViewModel
            {
                Id = pc.Id,
                Name = pc.Name
            });

            return Ok(viewModel);
        }
    }
}

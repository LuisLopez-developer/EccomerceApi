using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Model.ProductModel.CreateModel;
using EccomerceApi.Model.ProductModel.EditModel;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EccomerceApi.Controllers.Product
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

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _product.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductCreateModel productCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _product.CreateAsync(productCreateModel);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCreateModel productEditModel)
        {
            var existingPorduct = await _product.UpdateAsync(id, productEditModel);
            if (!existingPorduct)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("changeState/{idProduct}")]
        public async Task<IActionResult> ChangeState(int idProduct)
        {
            var existingProduct = await _product.ChangeStateAsync(idProduct);
            if (!existingProduct)
            {
                return BadRequest();
            }

            return NoContent();

        }

    }
}

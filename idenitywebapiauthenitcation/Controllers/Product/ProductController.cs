using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Model;
using EccomerceApi.Model.ProductModel.CreateModel;
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
        [HttpGet("GetPagedProducts")]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = "",
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize should be greater than 0.");
            }

            var pagedResult = await _product.GetLeakedProductsAsync(page, pageSize, searchTerm, startDate, endDate);

            var response = new
            {
                pagedResult.TotalItems,
                pagedResult.TotalPages,
                CurrentPage = page,
                PageSize = pageSize,
                pagedResult.Items
            };

            return Ok(response);
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

        //API'S enfocadas para la web del eccomerce
        [HttpGet("MostValablesProducts")]
        public async Task<IActionResult> GetMostValueblesProducts()
        {
            var response = await _product.GetMostValuableProductCatalog();
            return Ok(response);
        }

        [HttpGet("GetProductCatalogWithFilters")]
        public async Task<IActionResult> GetProductCatalogWithFilters(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = "",
            [FromQuery] int? brandId = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] string? model = null,
            [FromQuery] decimal? minimumPrice = null,
            [FromQuery] decimal? maximunPrice = null
        )
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize should be greater than 0.");
            }
            
            var result = await _product.GetProductCatalogWithFiltersAsync(page, pageSize, searchTerm, brandId, categoryId, model, minimumPrice, maximunPrice);

            var response = new
            {
                result.TotalItems,
                result.TotalPages,
                CurrentPage = page,
                PageSize = pageSize,
                result.Items
            };

            return Ok(response);
        }

        [HttpGet("GetProductInformationAsync")]
        public async Task<IActionResult> GetProductInformationAsync(int id) 
        {
            var result = await _product.GetProductInformationAsync(id);

            if(result == null)
            {
                return NotFound("No se pudo encontrar el producto");
            }
            return Ok(result);
        }
    }
}

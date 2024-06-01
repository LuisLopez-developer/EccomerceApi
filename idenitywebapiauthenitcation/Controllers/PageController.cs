using EccomerceApi.Data;
using EccomerceApi.Model.ProductModel.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IdentityDbContext _identityDbContext;
        public PageController(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchTerm = "")
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize should be greater than 0.");
            }

            var query = _identityDbContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductCategory)
                .Include(p => p.State)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) ||
                                         p.ProductBrand.Name.Contains(searchTerm) ||
                                         p.ProductCategory.Name.Contains(searchTerm));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var products = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Existence = p.Existence,
                    CategoryName = p.ProductCategory.Name,
                    BrandName = p.ProductBrand.Name,
                    Price = p.Price,
                    Cost = p.Cost,
                    StateName = p.State.Name
                })
                .ToListAsync();

            var response = new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = products
            };

            return Ok(response);
        }
    }
}

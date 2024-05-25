using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace EccomerceApi.Controllers
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

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _prodCategoryService.GetAllAsync();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reponse = await _prodCategoryService.GetByIdAsync(id);
            return Ok(reponse);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ProductCategory productCategory)
        {
            var respone = await _prodCategoryService.CreateAsync(productCategory);
            return CreatedAtAction(nameof(GetById), new { id = respone.Id }, respone);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProductCategory productCategory)
        {
            var existingBlog = await _prodCategoryService.UpdateAsync(id, productCategory);
            if (!existingBlog)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await _prodCategoryService.DeleteAsync(id);
            if (!blog)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}

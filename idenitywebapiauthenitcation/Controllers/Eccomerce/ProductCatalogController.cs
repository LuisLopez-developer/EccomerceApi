using EccomerceApi.Interfaces.ProductIntefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers.Eccomerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCatalogController : ControllerBase
    {
        private readonly IProduct _product;

        public ProductCatalogController(IProduct product)
        {
            _product = product;
        }


    }
}

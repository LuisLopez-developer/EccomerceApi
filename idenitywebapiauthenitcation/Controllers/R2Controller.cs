using Amazon.S3.Model;
using EccomerceApi.Interfaces;
using EccomerceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EccomerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class R2Controller : ControllerBase
    {
        private readonly ICloudflare _cloudflare;

        public R2Controller(ICloudflare cloudflare)
        {
            _cloudflare = cloudflare;
        }

        [HttpGet]
        [Route("ListBuckets")]
        public async Task<IActionResult> ListBuckets()
        {
            try
            {
                var buckets = await _cloudflare.ListBucketsAsync();
                return Ok(buckets);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        [Route("UploadObject")]
        public async Task<IActionResult> UploadObject(IFormFile file)
        {
            try
            {
                var url = await _cloudflare.UploadObjectAsync(file);
                return Ok(url);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}

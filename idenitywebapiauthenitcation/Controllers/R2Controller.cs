using Amazon.S3;
using EccomerceApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("ListBuckets")]
        public async Task<IActionResult> ListBuckets()
        {
            try
            {
                var buckets = await _cloudflare.ListBucketsAsync();
                return Ok(buckets);
            }
            catch (AmazonS3Exception s3Ex)
            {
                // Manejo de errores específicos de Amazon S3
                Console.WriteLine($"AmazonS3Exception: {s3Ex.Message}");
                Console.WriteLine(s3Ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "S3 error");
            }
            catch (HttpRequestException httpEx)
            {
                // Manejo de errores específicos de HttpClient
                throw new ApplicationException("HTTP request error", httpEx);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); // Cambia Console.WriteLine(e.Message) a e.ToString() para obtener más detalles
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("El archivo está vacío o no se proporcionó.");

            if (file.Length > 2 * 1024 * 1024) // 2 MB en bytes
                throw new ArgumentException("El tamaño del archivo excede el límite permitido.");

            return await _cloudflare.UploadObjectAsync(file);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([Required] IFormFile file)
        {
            try
            {
                var url = await UploadFile(file);
                return Ok(url);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {e.Message}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("UploadImages")]
        public async Task<IActionResult> UploadImages([Required] List<IFormFile> files)
        {
            try
            {
                var urls = await Task.WhenAll(files.Select(UploadFile));
                return Ok(urls);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {e.Message}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteObject")]
        public async Task<IActionResult> DeleteImage(string url)
        {
            try
            {
                await _cloudflare.DeleteObjectByUrlAsync(url);
                return Ok($"El objeto con clave '{url}' ha sido eliminado exitosamente.");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {e.Message}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("DeleteObjects")]
        public async Task<IActionResult> DeleteObjects(List<string> urls)
        {
            try
            {
                await _cloudflare.DeleteObjectsByUrlAsync(urls);
                return Ok("Los objetos han sido eliminados exitosamente.");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {e.Message}");
            }
        }
    }
}

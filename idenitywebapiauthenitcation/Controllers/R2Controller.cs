using Amazon.S3;
using Amazon.S3.Model;
using EccomerceApi.Interfaces;
using EccomerceApi.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadObject(IFormFile file)
        {
            try
            {
                // Validar que el archivo no sea nulo y tenga contenido
                if (file == null || file.Length == 0)
                {
                    return BadRequest("El archivo está vacío o no se proporcionó.");
                }

                // Validar el tamaño máximo del archivo (por ejemplo, 2 MB)
                if (file.Length > 2 * 1024 * 1024) // 2 MB en bytes
                {
                    return BadRequest("El tamaño del archivo excede el límite permitido.");
                }

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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error, {e.Message}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("UploadImages")]
        public async Task<IActionResult> UploadObjects(List<IFormFile> files)
        {
            try
            {
                // Validar que se hayan proporcionado archivos
                if (files == null || files.Count == 0)
                {
                    return BadRequest("No se proporcionaron archivos.");
                }

                var urls = new List<string>();

                foreach (var file in files)
                {
                    // Validar que el archivo no sea nulo y tenga contenido
                    if (file == null || file.Length == 0)
                    {
                        return BadRequest("Uno o más archivos están vacíos o no se proporcionaron.");
                    }

                    // Validar el tamaño máximo del archivo (por ejemplo, 2 MB)
                    if (file.Length > 2 * 1024 * 1024) // 2 MB en bytes
                    {
                        return BadRequest($"El tamaño de uno o más archivos excede el límite permitido: {file.FileName}");
                    }

                    var url = await _cloudflare.UploadObjectAsync(file);
                    urls.Add(url);
                }

                return Ok(urls);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error, {e.Message}");
            }
        }
    }
}

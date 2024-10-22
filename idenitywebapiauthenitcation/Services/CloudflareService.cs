using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using EccomerceApi.Interfaces;

namespace EccomerceApi.Services
{
    public class CloudflareService : ICloudflare
    {
        private readonly AmazonS3Client _s3Client;
        private readonly long _maxFileSize = 500 * 1024; // 500 KB
        private readonly string[] _allowedFileTypes = { "image/jpeg", "image/png", "image/webp" };

        public CloudflareService()
        {
            _s3Client = S3client();
        }

        private AmazonS3Client S3client()
        {
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var serviceUrl = Environment.GetEnvironmentVariable("SERVICE_CLOUDFLARE_R2_URL");

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var s3Client = new AmazonS3Client(credentials, new AmazonS3Config
            {
                ServiceURL = serviceUrl,
            });

            return s3Client;
        }

        public async Task<List<string>> ListBucketsAsync()
        {
            var res = await _s3Client.ListBucketsAsync();
            return res.Buckets.Select(b => b.BucketName).ToList();
        }

        public async Task<string> UploadObjectAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("El archivo está vacío o no se proporciona");
            }

            if (file.Length > _maxFileSize)
            {
                throw new ArgumentException("El tamaño del archivo excede el límite máximo de 500 KB");
            }

            if (!_allowedFileTypes.Contains(file.ContentType))
            {
                throw new ArgumentException("No se permite ningún tipo de archivo. Solo se admiten JPEG, PNG y WebP.");
            }

            var filepath = Path.GetTempFileName();
            try
            {
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var request = new PutObjectRequest
                {
                    FilePath = filepath,
                    BucketName = "eccomerce",
                    DisablePayloadSigning = true,
                    ContentType = file.ContentType,
                    Key = $"{file.FileName}"
                };

                await _s3Client.PutObjectAsync(request);

                return $"https://teamstarteight.work/{file.FileName}";
            }
            finally
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
        }

        public async Task DeleteObjectsByUrlAsync(List<string> urls)
        {
            if (urls == null || urls.Count == 0)
            {
                throw new ArgumentException("List of URLs cannot be null or empty");
            }

            foreach (var url in urls)
            {
                await DeleteObjectByUrlAsync(url);
            }
        }

        public async Task DeleteObjectByUrlAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty");
            }

            // Extraer el nombre del archivo de la URL
            string fileName = url.Substring(url.LastIndexOf('/') + 1);

            // Decodificar el nombre del archivo si es necesario
            fileName = Uri.UnescapeDataString(fileName);

            // Llamar al método para eliminar el objeto utilizando el nombre del archivo como clave
            await DeleteObjectAsync(fileName);
        }

        public async Task DeleteObjectAsync(string objectKey)
        {
            if (string.IsNullOrEmpty(objectKey))
            {
                throw new ArgumentException("Object key cannot be null or empty");
            }

            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = "eccomerce",
                Key = objectKey
            };

            await _s3Client.DeleteObjectAsync(deleteObjectRequest);
        }
    }
}

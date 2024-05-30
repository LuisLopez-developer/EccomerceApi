using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using EccomerceApi.Interfaces;

namespace EccomerceApi.Services
{
    public class CloudflareService : ICloudflare
    {
        private readonly AmazonS3Client _s3Client;

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
                throw new ArgumentException("File is empty or not provided");
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
                    Key = file.FileName
                };

                await _s3Client.PutObjectAsync(request);

                return "https://teamstarteight.work/" + file.FileName;
            }
            finally
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
        }
    }
  
}

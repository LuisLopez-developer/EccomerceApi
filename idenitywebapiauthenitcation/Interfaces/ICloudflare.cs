namespace EccomerceApi.Interfaces
{
    public interface ICloudflare
    {
        Task<List<string>> ListBucketsAsync();
        Task<string> UploadObjectAsync(IFormFile file);
    }
}

namespace EccomerceApi.Model.ProductModel.ViewModel
{
    public class ProductPhotoViewModel
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public required int ProductId { get; set; }
    }

    public class ProductPhotoEccomerceViewModel
    {
        public required string Url { get; set; }
        public bool IsMain { get; set; }
    }
}

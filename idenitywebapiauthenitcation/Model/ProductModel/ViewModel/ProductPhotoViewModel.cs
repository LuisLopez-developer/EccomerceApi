namespace EccomerceApi.Model.ProductModel.ViewModel
{
    public class ProductPhotoViewModel
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public required int ProductId { get; set; }
    }
}

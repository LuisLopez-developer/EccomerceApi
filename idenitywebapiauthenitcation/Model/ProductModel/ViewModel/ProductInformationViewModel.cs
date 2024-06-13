namespace EccomerceApi.Model.ProductModel.ViewModel
{
    public class ProductInformationViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Existence { get; set; }
        public string? Description { get; set; }
        public int ProductBrandId { get; set; }

        public List<ProductPhotoEccomerceViewModel>? Photos { get; set; }
        public ProductSpecificationViewModel? Specifications { get; set; }
    }
}

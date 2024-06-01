using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Model.ProductModel.EditModel
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required int StateId { get; set; }
        public required decimal Price { get; set; }
        public required decimal? Cost { get; set; }
        public required int Existence { get; set; }
        public bool IsVisible { get; set; } = false;
        public string? Description { get; set; }
        public string BarCode { get; set; } = "sn";
        public required int ProductBrandId { get; set; }
        public required int ProductCategoryId { get; set; }

        public List<ProductPhotoViewModel>? Photos { get; set; }
        public ProductSpecificationViewModel? Specifications { get; set; }
    }
}

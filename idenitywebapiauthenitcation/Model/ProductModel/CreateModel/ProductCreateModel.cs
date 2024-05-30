using EccomerceApi.Entity;
using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Model.ProductModel.CreateModel
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public int? StateId { get; set; }
        public DateTime? Date { get; set; }
        public required decimal Price { get; set; }
        public required decimal? Cost { get; set; }
        public required int Existence { get; set; }
        public required int ProductBrandId { get; set; }
        public required int ProductCategoryId { get; set; }

        public List<ProductPhotoViewModel>? Photos { get; set; }
        public ProductSpecificationViewModel? Specifications { get; set; }
    }
}

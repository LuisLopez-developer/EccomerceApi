namespace EccomerceApi.Model.ProductModel.ViewModel
{
    public class ProductCatalogViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string ImageUrl { get; set; }
    }
}

namespace EccomerceApi.Model.ProductModel.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Existence { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public string? StateName { get; set; }
    }
}


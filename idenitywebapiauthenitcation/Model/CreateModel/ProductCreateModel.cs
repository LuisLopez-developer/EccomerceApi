namespace EccomerceApi.Model.CreateModel
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? IdState { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public int? Existence { get; set; }
        public int? ProductBrandId { get; set; }
        public int? IdProductCategory { get; set; }
    }
}

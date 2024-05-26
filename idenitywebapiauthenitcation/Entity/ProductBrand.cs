namespace EccomerceApi.Entity
{
    public class ProductBrand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}

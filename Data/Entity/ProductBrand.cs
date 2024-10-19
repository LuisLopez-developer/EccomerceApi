namespace Data.Entity
{
    public class ProductBrand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();

    }
}

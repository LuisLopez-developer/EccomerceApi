namespace EccomerceApi.Entity
{
    public class ProductPhoto
    {
        public int Id { get; set; }
        public required string Url { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}

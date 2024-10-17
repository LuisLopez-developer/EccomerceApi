namespace Data.Entity
{
    public class ProductPhoto
    {
        public int Id { get; set; }
        public required string FileName { get; set; } = "SinNombre";
        public required string Url { get; set; }
        public bool IsMain { get; set; } = false;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

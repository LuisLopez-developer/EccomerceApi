namespace Models
{
    public class ProductPhotoModel
    {
        public int Id { get; set; }
        public required string FileName { get; set; } = "SinNombre";
        public required string Url { get; set; }
        public bool IsMain { get; set; } = false;
        public int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}

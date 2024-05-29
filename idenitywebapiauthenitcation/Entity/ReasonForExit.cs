namespace EccomerceApi.Entity
{
    public class ReasonForExit
    {
        public int Id { get; set; }
        public required string Reason {  get; set; }

        public virtual ICollection<ProductOutput> ProductOutputs { get; set; } = new List<ProductOutput>();

    }
}

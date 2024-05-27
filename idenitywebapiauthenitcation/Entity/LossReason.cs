namespace EccomerceApi.Entity
{
    public class LossReason
    {
        public int id { get; set; }
        public string reason { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        public virtual ICollection<LostDetail> LostDetails { get; set; } = new List<LostDetail>();

    }
}

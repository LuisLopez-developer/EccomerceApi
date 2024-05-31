namespace EccomerceApi.Entity
{
    public class EntryType
    {
        public int Id { get; set; }
        public required string Type { get; set; }

        public virtual List<EntryDetail> EntryDetails { get; set; } = new List<EntryDetail>();
    }
}

namespace Data.Entity
{
    public class EntryType
    {
        public int Id { get; set; }
        public required string Type { get; set; }

        public virtual List<Entry> EntryDetails { get; set; } = new List<Entry>();
    }
}

namespace Models
{
    public class EntryTypeModel
    {
        public int Id { get; set; }
        public required string Type { get; set; }

        public virtual List<EntryModel> EntryDetails { get; set; } = new List<EntryModel>();
    }
}

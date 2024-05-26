namespace EccomerceApi.Model.CreateModel
{
    public class EntryCreateModel
    {
        public int IdEntry { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Total { get; set; }
        public int? IdState { get; set; }

        public int IdEntryDetail { get; set; }
        public decimal? UnitCost { get; set; }
        public int? Amount { get; set; }
        public int? IdProduct { get; set; }
    }
}

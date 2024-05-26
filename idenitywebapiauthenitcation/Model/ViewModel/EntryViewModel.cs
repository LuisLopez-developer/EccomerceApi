namespace EccomerceApi.Model.ViewModel
{
    public class EntryViewModel
    {
        public int IdEntry { get; set; }

        public DateTime? Date { get; set; }

        public string? Name { get; set; }

        public decimal? UnitCost { get; set; }

        public int? Amount { get; set; }

        public decimal? Total { get; set; }

        public int? Existence { get; set; }
    }
}

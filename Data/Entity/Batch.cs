namespace Data.Entity
{
    public class Batch
    {
        public int Id { get; set; }

        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime EntryDate { get; set; }
        public required decimal Cost { get; set; }

        public required int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

        public virtual EntryDetail EntryDetails { get; set; } = new EntryDetail();

    }
}

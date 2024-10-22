namespace Models
{
    public class BatchModel
    {
        public int Id { get; set; }

        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime EntryDate { get; set; }
        public required decimal Cost { get; set; }

        public required int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

        public virtual EntryDetailModel EntryDetails { get; set; } = new EntryDetailModel();

    }
}

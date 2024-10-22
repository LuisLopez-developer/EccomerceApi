namespace EccomerceApi.Model
{
    public class BatchViewModel
    {
        public int Id { get; set; }
        public int InitialQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime EntryDate { get; set; }
        public required decimal Cost { get; set; }
        public int ProductId { get; set; }
    }
}

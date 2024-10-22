namespace Models;

public partial class EntryDetailModel
{
    public int Id { get; set; }
    public decimal? UnitCost { get; set; }
    public int? Amount { get; set; }
    public string? Description { get; set; }

    public int? EntryId { get; set; }
    public virtual EntryModel? Entry { get; set; }

    public int? ProductId { get; set; }
    public virtual ProductModel? Product { get; set; }

    public int BatchId { get; set; }
    public BatchModel Batch { get; set; }
}

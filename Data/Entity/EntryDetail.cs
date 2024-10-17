namespace Data.Entity;

public partial class EntryDetail
{
    public int Id { get; set; }
    public decimal? UnitCost { get; set; }
    public int? Amount { get; set; }
    public string? Description { get; set; }

    public int? EntryId { get; set; }
    public virtual Entry? Entry { get; set; }

    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public int BatchId { get; set; }
    public Batch Batch { get; set; }
}

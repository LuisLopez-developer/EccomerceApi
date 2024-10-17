namespace Data.Entity;
public partial class SaleDetail
{
    public int Id { get; set; }
    public decimal? UnitCost { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? Amount { get; set; }
    public decimal? Subtotal { get; set; }
    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public int? SaleId { get; set; }
    public virtual Sale? Sale { get; set; }

    public int BatchId { get; set; }
    public Batch? Batch { get; set; }

}

namespace Data.Entity;

public partial class Sale
{
    public int Id { get; set; }
    public decimal? Total { get; set; }
    public DateTime? Date { get; set; }

    public int? StateId { get; set; }
    public virtual State? State { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}

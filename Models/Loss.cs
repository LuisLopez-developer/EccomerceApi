namespace Models;

public partial class Loss
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.Now;
    public decimal? Total { get; set; }

    public int? StateId { get; set; }
    public virtual StateModel? State { get; set; }

    public virtual ICollection<LostDetail> LostDetails { get; set; } = new List<LostDetail>();
}

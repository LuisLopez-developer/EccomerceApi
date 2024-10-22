namespace Models;
public partial class LostDetailModel
{
    public int Id { get; set; }
    public decimal? UnitCost { get; set; }
    public required int Amount { get; set; }
    public string? Description { get; set; } = string.Empty;
    public int? LossId { get; set; }
    public virtual LossModel? Loss { get; set; }

    public int? ProductId { get; set; }
    public virtual ProductModel? Product { get; set; }

    public int? LossReasonId { get; set; }
    public virtual LossReasonModel LossReason { get; set; }
}

using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;
public partial class LostDetail
{
    public int Id { get; set; }
    public decimal? UnitCost { get; set; }
    public int? Amount { get; set; }

    public int? LossId { get; set; }
    public virtual Loss? Loss { get; set; }

    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public int reasonId { get; set; }
    public virtual LossReason LossReason { get; set; }
}

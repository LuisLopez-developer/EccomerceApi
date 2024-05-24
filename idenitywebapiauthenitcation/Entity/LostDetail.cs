using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;
public partial class LostDetail
{
    public int Id { get; set; }

    public int? IdLoss { get; set; }

    public decimal? UnitCost { get; set; }

    public int? IdProduct { get; set; }

    public int? Amount { get; set; }

    public virtual Loss? IdLossNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}

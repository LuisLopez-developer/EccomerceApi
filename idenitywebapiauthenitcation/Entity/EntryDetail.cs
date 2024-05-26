using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;

public partial class EntryDetail
{
    public int Id { get; set; }
    public decimal? UnitCost { get; set; }
    public int? Amount { get; set; }

    public int? EntryId { get; set; }
    public virtual Entry? Entry { get; set; }

    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }
}

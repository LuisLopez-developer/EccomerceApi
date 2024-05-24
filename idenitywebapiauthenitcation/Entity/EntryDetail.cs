using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;

public partial class EntryDetail
{
    public int Id { get; set; }

    public int? IdEntry { get; set; }

    public decimal? UnitCost { get; set; }

    public int? Amount { get; set; }

    public int? IdProduct { get; set; }

    public virtual Entry? IdEntryNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}

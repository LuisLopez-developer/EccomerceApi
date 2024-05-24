using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;

public partial class Entry
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Total { get; set; }

    public int? IdState { get; set; }

    public virtual ICollection<EntryDetail> EntryDetails { get; set; } = new List<EntryDetail>();

    public virtual State? IdStateNavigation { get; set; }
}

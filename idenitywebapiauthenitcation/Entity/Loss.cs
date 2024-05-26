using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;

public partial class Loss
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public decimal? Total { get; set; }

    public int? StateId { get; set; }
    public virtual State? State { get; set; }

    public virtual ICollection<LostDetail> LostDetails { get; set; } = new List<LostDetail>();
}

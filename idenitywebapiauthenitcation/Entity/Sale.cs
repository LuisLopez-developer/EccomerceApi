using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;

public partial class Sale
{
    public int Id { get; set; }

    public decimal? Total { get; set; }

    public DateTime? Date { get; set; }

    public int? IdState { get; set; }

    public virtual State? IdStateNavigation { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}

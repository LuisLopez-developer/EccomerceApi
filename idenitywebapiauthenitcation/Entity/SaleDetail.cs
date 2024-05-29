using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;
public partial class SaleDetail
{
    public int Id { get; set; }
    public decimal? UnitCost { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? Amount { get; set; }

    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public int? SaleId { get; set; }
    public virtual Sale? Sale { get; set; }

    public virtual ProductOutput ProductOutputs { get; set; } = new ProductOutput();
}

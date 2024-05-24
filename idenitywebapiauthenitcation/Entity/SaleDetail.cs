using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;
public partial class SaleDetail
{
    public int Id { get; set; }

    public int? IdSale { get; set; }

    public decimal? UnitCost { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? Amount { get; set; }

    public int? IdProduct { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Sale? IdSaleNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public int? Existence { get; set; }

        public int? IdState { get; set; }
        public virtual State IdStateNavigation { get; set; }

        public int? ProductBrandId { get; set; }
        public virtual ProductBrand ProductBrand { get; set; }

        public int? IdProductCategory { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<EntryDetail> EntryDetails { get; set; } = new List<EntryDetail>();
        public virtual ICollection<LostDetail> LostDetails { get; set; } = new List<LostDetail>();
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}

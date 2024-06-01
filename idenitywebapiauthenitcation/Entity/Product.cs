using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity
{
    public partial class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; } //SKU
        public DateTime? Date { get; set; }
        public DateTime UpdateAt { get; set; }
        public required decimal Price { get; set; }
        public decimal? Cost { get; set; }
        public required int Existence { get; set; }
        public bool IsVisible { get; set; } = false;
        public string? Description { get; set; }
        public string BarCode { get; set; } = "sn";

        public required int StateId { get; set; }
        public virtual State State { get; set; }

        public required int ProductBrandId { get; set; }
        public virtual ProductBrand ProductBrand { get; set; }

        public required int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<EntryDetail> EntryDetails { get; set; } = new List<EntryDetail>();
        public virtual ICollection<LostDetail> LostDetails { get; set; } = new List<LostDetail>();
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; } = new List<ProductPhoto>();
        public virtual ProductSpecification ProductSpecifications { get; set; } = new ProductSpecification();
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public partial class ProductModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public required string Name { get; set; }

        [StringLength(16, MinimumLength = 4)]
        public required string SKU { get; set; } //SKU
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; }
        public required decimal Cost { get; set; } = 0;
        public required decimal Price { get; set; } = 0;
        public required int Existence { get; set; } = 0;
        public bool IsVisible { get; set; } = false;

        [StringLength(5000)]
        public string? Description { get; set; }

        [StringLength(16, MinimumLength = 2)]
        public string BarCode { get; set; } = "sn";

        public required int StateId { get; set; }
        public virtual StateModel State { get; set; }

        public required int ProductBrandId { get; set; }
        public virtual ProductBrandModel ProductBrand { get; set; }

        public required int ProductCategoryId { get; set; }
        public virtual ProductCategoryModel ProductCategory { get; set; }

        public virtual ICollection<EntryDetail> EntryDetails { get; set; } = new List<EntryDetail>();
        public virtual ICollection<LostDetail> LostDetails { get; set; } = new List<LostDetail>();
        public virtual ICollection<ProductPhotoModel> ProductPhotos { get; set; } = new List<ProductPhotoModel>();
        public virtual ProductSpecificationModel ProductSpecifications { get; set; } = new ProductSpecificationModel();
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
    }
}

using System.ComponentModel.DataAnnotations;
using EccomerceApi.Herlpers;
using EccomerceApi.Model.ProductModel.ViewModel;

namespace EccomerceApi.Model.ProductModel.CreateModel
{
    public class ProductCreateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El SKU es obligatorio.")]
        public required string SKU { get; set; } // Stock Keeping Unit

        [Required(ErrorMessage = "El ID del estado es obligatorio.")]
        public int StateId { get; set; }

        public DateTime Date { get; set; } = getTimePeruHelper.GetCurrentTimeInPeru(); // Obtener la hora actual de Perú

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El costo no puede ser negativo.")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "La existencia es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa.")]
        public int Existence { get; set; }

        public bool IsVisible { get; set; } = false;

        [StringLength(5000)]
        public string? Description { get; set; }

        [StringLength(16, MinimumLength = 2)]
        public string BarCode { get; set; } = "sn";

        [Required(ErrorMessage = "El ID de la marca del producto es obligatorio.")]
        public int ProductBrandId { get; set; }

        [Required(ErrorMessage = "El ID de la categoría del producto es obligatorio.")]
        public int ProductCategoryId { get; set; }

        public List<ProductPhotoViewModel>? Photos { get; set; }
        public ProductSpecificationViewModel? Specifications { get; set; }
    }
}

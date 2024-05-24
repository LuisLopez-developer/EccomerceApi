using System.Collections.Generic;

namespace EccomerceApi.Entity
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Colección de productos
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

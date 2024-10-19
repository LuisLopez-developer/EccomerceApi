namespace EnterpriseLayer
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public string SKU { get; }
        public DateTime CreateAt { get; }
        public DateTime UpdateAt { get; }
        public decimal Cost { get; set; }
        public decimal Price { get; }
        public int Existence { get; set; }
        public bool IsVisible { get; }
        public string Description { get; }
        public string BarCode { get; }
        public int StateId { get; }
        public int ProductBrandId { get; }
        public int ProductCategoryId { get; }

        public Product(int id, string name, string SKU, DateTime createAt, DateTime updateAt, decimal cost, decimal price, int existence, bool isVisible, string description, string barCode, int stateId, int productBrandId, int productCategoryId)
        {
            Id = id;
            Name = name;
            this.SKU = SKU;
            CreateAt = createAt;
            UpdateAt = updateAt;
            Cost = cost;
            Price = price;
            Existence = existence;
            IsVisible = isVisible;
            Description = description;
            BarCode = barCode;
            StateId = stateId;
            ProductBrandId = productBrandId;
            ProductCategoryId = productCategoryId;
        }

    }
}

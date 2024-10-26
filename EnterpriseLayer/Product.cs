namespace EnterpriseLayer
{
    public class Product
    {
        private List<ProductPhoto> productPhotos;

        public int Id { get; }
        public string Name { get; set; }
        public string SKU { get; }
        public DateTime CreateAt { get; }
        public DateTime UpdateAt { get; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int Existence { get; set; }
        public bool IsVisible { get; }
        public string Description { get; }
        public string BarCode { get; set; }
        public int StateId { get; }
        public int ProductBrandId { get; }
        public int ProductCategoryId { get; }
        public List<ProductPhoto> ProductPhotos { get; set; }

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

        public Product(int id, string name, string SKU, DateTime createAt, DateTime updateAt, decimal cost, decimal price, int existence, bool isVisible, string description, string barCode, int stateId, int productBrandId, int productCategoryId, List<ProductPhoto> productPhotos) : this(id, name, SKU, createAt, updateAt, cost, price, existence, isVisible, description, barCode, stateId, productBrandId, productCategoryId)
        {
            this.productPhotos = productPhotos;
        }
    }

    public class ProductPhoto
    {
        public int Id { get; }
        public string FileName { get; }
        public string Url { get; }
        public bool IsMain { get; }
        public int ProductId { get; }
        public Product Product { get; }

        public ProductPhoto(int id, string fileName, string url, bool isMain, int productId, Product product)
        {
            Id = id;
            FileName = fileName;
            Url = url;
            IsMain = isMain;
            ProductId = productId;
            Product = product;
        }

        public ProductPhoto(int id, string fileName, string url, bool isMain)
        {
            Id = id;
            FileName = fileName;
            Url = url;
            IsMain = isMain;
        }
    }

}

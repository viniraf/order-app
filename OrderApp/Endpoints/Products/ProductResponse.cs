namespace OrderApp.Endpoints.Products
{
    public class ProductResponse
    {
        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public bool HasStock { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }
    }
}

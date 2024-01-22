namespace OrderApp.Endpoints.Products
{
    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool HasStock { get; set; }

        public bool Active { get; set; }
    }
}

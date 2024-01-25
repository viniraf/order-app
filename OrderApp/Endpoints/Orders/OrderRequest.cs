namespace OrderApp.Endpoints.Orders
{
    public class OrderRequest
    {
        public List<Guid> ProductsIds { get; set; }

        public string DeliveryAddress { get; set; }
    }
}
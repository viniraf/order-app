namespace OrderApp.Domain.Order
{
    public class OrderClass : Entity
    {

        public string ClientId { get; private set; } = string.Empty;

        public List<Product> Products { get; private set; }

        public decimal Total { get; set; }

        public string DeliveryAddress { get; private set; } = string.Empty;

        public OrderClass() { }

        public OrderClass(string clientId, string clientName, List<Product> products, string deliveryAddress)
        {
            ClientId = clientId;
            Products = products;
            Total = 0;
            DeliveryAddress = deliveryAddress;
            CreatedBy = clientName;
            EditedBy = clientName;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            foreach (var item in products)
            {
                Total += item.Price;
            }

            var contract = new Contract<OrderClass>()
                .IsNotNullOrEmpty(clientId, "ClientId")
                .IsNotNullOrEmpty(clientName, "ClientName")
                .IsNotNull(products, "Products")
                .IsGreaterThan(Total, 0, "Price")
                .IsNotNullOrEmpty(deliveryAddress, "DeliveryAddress");
            AddNotifications(contract);

        }
    }
}

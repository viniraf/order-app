namespace OrderApp.Domain.Order
{
    public class Order : Entity
    {

        public string ClientId { get; private set; } = string.Empty;

        public List<Product> Products { get; private set; }

        public decimal Total { get; set; }

        public string DeliveryAddress { get; private set; } = string.Empty;

        public Order() { }

        public Order(string clientId, string clientName, List<Product> products, decimal total, string deliveryAddress)
        {
            ClientId = clientId;
            Products = products;
            Total = total;
            DeliveryAddress = deliveryAddress;
            CreatedBy = clientName;
            EditedBy = clientName;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Total = 0;
            foreach (var item in products)
            {
                Total += item.Price;
            }

            var contract = new Contract<Order>()
                .IsNotNullOrEmpty(clientId, "ClientId")
                .IsNotNullOrEmpty(clientName, "ClientName")
                .IsNotNull(products, "Products")
                .IsGreaterThan(total, 0, "Price")
                .IsNotNullOrEmpty(deliveryAddress, "DeliveryAddress");
            AddNotifications(contract);

        }
    }
}

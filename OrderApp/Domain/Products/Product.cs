namespace OrderApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public string Description { get; set; } = string.Empty;

    public Category? Category { get; set; } 

    public bool HasStock { get; set; }

    public bool Active { get; set; }

    public Product(string name)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name");

        AddNotifications(contract);

        Name = name;
        Active = true;
    }
}
